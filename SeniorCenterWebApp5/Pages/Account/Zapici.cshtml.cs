//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using SeniorCenterWebApp.Data;
//using SeniorCenterWebApp.Models; // Ваши модели
//using System.Collections.Generic;
//using System.Linq;

//namespace SeniorCenterWebApp.Pages.Account //старое записи
//{
//    public class ZapiciModel : PageModel
//    {
//        private readonly DataContext _context;

//        public ZapiciModel(DataContext context)
//        {
//            _context = context;
//        }

//        // Для фильтрации
//        [BindProperty(SupportsGet = true)]
//        public string SelectedVenue { get; set; }

//        public List<SelectListItem> VenueOptions { get; set; }

//        public List<Activity> Activities { get; set; }

//        public void OnGet()
//        {
//            // Получение всех уникальных мест
//            var venuesQuery = _context.Activities
//                .Select(a => a.Venue)
//                .Distinct()
//                .OrderBy(v => v)
//                .ToList();

//            VenueOptions = venuesQuery
//                .Select(v => new SelectListItem { Value = v, Text = v })
//                .ToList();

//            // Фильтрация по выбранному месту
//            var query = _context.Activities.AsQueryable();

//            if (!string.IsNullOrEmpty(SelectedVenue))
//            {
//                query = query.Where(a => a.Venue == SelectedVenue);
//            }

//            Activities = query.ToList();
//        }
//    }
//}


