using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SeniorCenterWebApp.Data;
using SeniorCenterWebApp.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace SeniorCenterWebApp.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly DataContext _context;

        public RegisterModel(DataContext context)
        {
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [StringLength(50, MinimumLength = 3)]
            public string Username { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [StringLength(100, MinimumLength = 6)]
            public string Password { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "Пароли не совпадают")]
            public string ConfirmPassword { get; set; }

            [EmailAddress]
            public string Email { get; set; }



            [StringLength(100)]
            public string FullName { get; set; }

            [Required(ErrorMessage = "Дата рождения обязательна")]
            [DataType(DataType.Date)]
            [Display(Name = "Дата рождения")]
            public DateTime DateOfBirth { get; set; }  //новое
        }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            var stopwatch = Stopwatch.StartNew(); //доб лог

            if (!ModelState.IsValid)
                return Page();

            // ПРОВЕРКА ВОЗРАСТА
            var today = DateTime.Today;
            var age = today.Year - Input.DateOfBirth.Year;

            if (Input.DateOfBirth > today.AddYears(-age))
                age--;

            if (age < 17 || age > 110)
            {
                ModelState.AddModelError("Input.DateOfBirth", "Возраст должен быть от 17 до 110 лет");
                return Page();
            }




            
     


            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == Input.Username);

            if (existingUser != null)
            {
                ModelState.AddModelError(string.Empty, "Пользователь с таким именем уже существует");
                return Page();
            }

            var salt = GenerateSalt();
            var hash = HashPassword(Input.Password, salt);

            var user = new Models.User
            {
                Username = Input.Username,
                PasswordHash = hash,
                Salt = salt,
                Email = Input.Email,
                FullName = Input.FullName,
                DateOfBirth = DateTime.SpecifyKind(Input.DateOfBirth, DateTimeKind.Utc), //дата рожденния
                RegistrationDate = DateTime.UtcNow,
                Role = "User" //автоматически

            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            stopwatch.Stop(); //доб лог
            Console.WriteLine($"User '{Input.Username}' registered in {stopwatch.ElapsedMilliseconds} ms");

            return RedirectToPage("/Account/Login");
        }

        private string GenerateSalt()
        {
            byte[] saltBytes = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }

        private string HashPassword(string password, string salt)
        {
            using var sha256 = SHA256.Create();
            var combined = Encoding.UTF8.GetBytes(password + salt);
            var hash = sha256.ComputeHash(combined);
            return Convert.ToBase64String(hash);
        }



    }
}











