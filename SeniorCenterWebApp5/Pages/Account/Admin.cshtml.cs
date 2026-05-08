using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SeniorCenterWebApp.Data;
using SeniorCenterWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeniorCenterWebApp.Pages.Account
{
    public class AdminModel : PageModel
    {
        private readonly DataContext _context;

        public AdminModel(DataContext context)
        {
            _context = context;
        }

        // Для поиска
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        // Список пользователей
        public List<User> Users { get; set; }

        // Список результатов тестов
        public List<TestResult> TestResults { get; set; }

        // Для добавления нового пользователя
        [BindProperty]
        public User NewUser { get; set; }

        public async Task OnGetAsync()
        {
            var usersQuery = _context.Users.AsNoTracking();

            // Поиск
            if (!string.IsNullOrEmpty(SearchString))
            {
                usersQuery = usersQuery.Where(u => u.Username.Contains(SearchString) || u.Email.Contains(SearchString));
            }

            Users = await usersQuery.ToListAsync();

            TestResults = await _context.TestResults
                .Include(tr => tr.User)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Устанавливаем дату регистрации
            NewUser.RegistrationDate = DateTime.Now;

            // Можно добавить соль и хэш пароля здесь, если требуется
            if (string.IsNullOrEmpty(NewUser.Salt))
                NewUser.Salt = Guid.NewGuid().ToString();

            _context.Users.Add(NewUser);
            await _context.SaveChangesAsync();

            return RedirectToPage(); // обновляем страницу
        }
    }
}