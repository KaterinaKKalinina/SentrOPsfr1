//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using Microsoft.EntityFrameworkCore;
//using SeniorCenterWebApp.Data;
//using SeniorCenterWebApp.Models;
//using System.Security.Claims;
//using System.Threading.Tasks;

//namespace SeniorCenterWebApp.Pages.Account
//{
//    [Authorize]
//    //[AllowAnonymous]
//    public class EnrollModel : PageModel
//    {
//        private readonly SeniorCenterDbContext _context;

//        public EnrollModel(SeniorCenterDbContext context)
//        {
//            _context = context;
//        }

//        [BindProperty]
//        public int ActivityId { get; set; }

//        public Activity Activity { get; set; }

//        [BindProperty]
//        public string PreferredDay { get; set; }

//        [BindProperty]
//        public string PreferredTime { get; set; }

//        public async Task<IActionResult> OnGetAsync(int activityId)
//        {
//            // Загружаем информацию о занятии по переданному ID
//            Activity = await _context.Activities.FindAsync(activityId);
//            if (Activity == null)
//                return NotFound();

//            ActivityId = activityId;
//            return Page();
//        }

//        public async Task<IActionResult> OnPostAsync()
//        {
//            // Проверяем наличие занятия
//            var activity = await _context.Activities.FindAsync(ActivityId);
//            if (activity == null)
//                return NotFound();

//            // Получить идентификатор текущего пользователя из Claims
//            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
//            if (string.IsNullOrEmpty(userIdStr))
//                return Challenge();

//            // Преобразуем строку в int
//            if (!int.TryParse(userIdStr, out int userId))
//                return Challenge();

//            // Находим пользователя по Id
//            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
//            if (user == null)
//                return Challenge();

//            // Создаем новую запись о записи на занятие
//            var userActivity = new UserActivity
//            {
//                UserId = user.Id,
//                ActivityId = ActivityId,
//                PreferredDay = PreferredDay,
//                PreferredTime = PreferredTime
//            };

//            // Добавляем в базу и сохраняем
//            _context.UserActivities.Add(userActivity);
//            await _context.SaveChangesAsync();


//            return RedirectToPage("/Account/Proverka", new { activityId = ActivityId });



//        }

       
//    }
//}

//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using Microsoft.EntityFrameworkCore;
//using SeniorCenterWebApp.Data;
//using SeniorCenterWebApp.Models;
//using System.Security.Claims;
//using System.Threading.Tasks;

//namespace SeniorCenterWebApp.Pages.Account
//{
//    [Authorize]
//    public class EnrollModel : PageModel
//    {
//        private readonly SeniorCenterDbContext _context;

//        public EnrollModel(SeniorCenterDbContext context)
//        {
//            _context = context;
//        }

//        [BindProperty]
//        public int ActivityId { get; set; }

//        public Activity Activity { get; set; }

//        [BindProperty]
//        public string PreferredDay { get; set; }

//        [BindProperty]
//        public string PreferredTime { get; set; }

//        public async Task<IActionResult> OnGetAsync(int activityId)
//        {
//            Activity = await _context.Activities.FindAsync(activityId);
//            if (Activity == null)
//                return NotFound();

//            ActivityId = activityId;
//            return Page();
//        }

//        public async Task<IActionResult> OnPostAsync()
//        {
//            var activity = await _context.Activities.FindAsync(ActivityId);
//            if (activity == null)
//                return NotFound();

//            // Получить строковый идентификатор пользователя
//            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
//            if (string.IsNullOrEmpty(userIdStr))
//                return Challenge();

//            // Преобразовать в int
//            if (!int.TryParse(userIdStr, out int userId))
//                return Challenge();

//            // Найти пользователя по int Id
//            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
//            if (user == null)
//                return Challenge();

//            var userActivity = new UserActivity
//            {
//                UserId = user.Id, // int
//                ActivityId = ActivityId,
//                PreferredDay = PreferredDay,
//                PreferredTime = PreferredTime
//            };

//            _context.UserActivities.Add(userActivity);
//            await _context.SaveChangesAsync();

//            return RedirectToPage("/Index");
//        }
//    }
//}



//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using Microsoft.EntityFrameworkCore;
//using SeniorCenterWebApp.Data;
//using SeniorCenterWebApp.Models;
//using System.Security.Claims;
//using System.Threading.Tasks;

//namespace SeniorCenterWebApp.Pages.Account
//{
//    [Authorize]

//    public class EnrollModel : PageModel
//    {
//        //public void OnGet()
//        //{
//        //}

//        private readonly SeniorCenterDbContext _context;

//        public EnrollModel(SeniorCenterDbContext context)
//        {
//            _context = context;
//        }

//        [BindProperty]
//        public int ActivityId { get; set; }

//        public Activity Activity { get; set; }

//        [BindProperty]
//        public string PreferredDay { get; set; }

//        [BindProperty]
//        public string PreferredTime { get; set; }

//        public async Task<IActionResult> OnGetAsync(int activityId)
//        {
//            Activity = await _context.Activities.FindAsync(activityId);
//            if (Activity == null)
//                return NotFound();

//            ActivityId = activityId;
//            return Page();
//        }

//        public async Task<IActionResult> OnPostAsync()
//        {
//            var activity = await _context.Activities.FindAsync(ActivityId);
//            if (activity == null)
//                return NotFound();

//            // Получить текущего пользователя по Claims
//            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
//            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id.ToString() == userIdStr);
//            if (user == null)
//                return Challenge();

//            var userActivity = new UserActivity
//            {
//                UserId = user.Id,
//                ActivityId = ActivityId,
//                PreferredDay = PreferredDay,
//                PreferredTime = PreferredTime
//            };

//            _context.UserActivities.Add(userActivity);
//            await _context.SaveChangesAsync();

//            return RedirectToPage("/Index");
//        }
//    }
//}
