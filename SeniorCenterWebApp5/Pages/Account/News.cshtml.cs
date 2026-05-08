using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SeniorCenterWebApp.Data;
using SeniorCenterWebApp.Models;

namespace SeniorCenterWebApp.Pages.Account
{
    public class NewsModel : PageModel
    {
        private readonly DataContext _context;

        public NewsModel(DataContext context)
        {
            _context = context;
        }

        // список новостей
        public List<New> NewsList { get; set; }

        // категории (как District)
        public List<string> Categories { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SelectedCategory { get; set; }

        public async Task OnGetAsync()
        {
            // получаем категории
            Categories = await _context.News
                .Select(n => n.Category)
                .Distinct()
                .ToListAsync();

            var query = _context.News.AsQueryable();

            // фильтр
            if (!string.IsNullOrEmpty(SelectedCategory) && SelectedCategory != "¬се")
            {
                query = query.Where(n => n.Category == SelectedCategory);
            }

            // сортировка по дате
            NewsList = await query
                .OrderByDescending(n => n.PublishDate)
                .ToListAsync();
        }
    }
}
