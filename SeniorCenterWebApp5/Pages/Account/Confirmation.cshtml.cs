//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using Microsoft.EntityFrameworkCore;
//using SeniorCenterWebApp.Data;
//using SeniorCenterWebApp.Models;
//using System.Security.Claims;
//using System.Threading.Tasks;

//namespace SeniorCenterWebApp.Pages.Account ////старое активиткс
//{
//    public class ConfirmationModel : PageModel
//    {
//        private readonly DataContext _context;

//        public ConfirmationModel(DataContext context)
//        {
//            _context = context;
//        }

//        [BindProperty]
//        public Activity Activity { get; set; }

//        public int ActivityId { get; set; }

//        public async Task<IActionResult> OnGetAsync(int activityId)
//        {
//            ActivityId = activityId;
//            Activity = await _context.Activities.FindAsync(activityId);
//            return Page();
//        }

//        public async Task<IActionResult> OnPostAsync()
//        {
//            // ѕолучение текущего пользовател€
//            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
//            if (userId == null)
//            {
//                return Unauthorized();
//            }

//            // ѕредполагаем, что пользователь авторизован и у него есть ID
//            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id.ToString() == userId);
//            if (user == null)
//            {
//                return Unauthorized();
//            }

//            // —оздаем новую запись в UserActivities
//            var userActivity = new UserActivities
//            {
//                UserId = userId,
//                ActivityId = Activity.Id,
//                UserId1 = user.Id, // предполагаетс€, что это внутренний Id
//                PreferredDay = Activity.ActivityDay,
//                PreferredTime = Activity.Time
//            };

//            _context.UserActivities.Add(userActivity);
//            await _context.SaveChangesAsync();

//            //return RedirectToPage("/Account/HobbyTest"); // перенаправление после подтверждени€
//            return RedirectToPage("/Account/Confirmation", new { activityId = Activity.Id });
//        }
//    }
//}