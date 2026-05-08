using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SeniorCenterWebApp.Data;
using SeniorCenterWebApp.Models;

namespace SeniorCenterWebApp.Pages.Account
{
    public class MeropriatiaModel : PageModel
    {
        private readonly DataContext _context;

        public MeropriatiaModel(DataContext context)
        {
            _context = context;
        }

        // Список мероприятий для отображения
        public List<Meropr> Meroprs { get; set; }

        // Список районов для фильтра
        public List<string> Districts { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SelectedDistrict { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SelectedTimeFilter { get; set; } = "Все"; //новоеее статусы

        [TempData]
        public string Message { get; set; }

        //public async Task OnGetAsync()
        //{
        //    // Получаем уникальные районы из таблицы Meroprs
        //    Districts = await _context.Meroprs
        //        .Select(m => m.District)
        //        .Distinct()
        //        .ToListAsync();

        //    // Получаем мероприятия, с фильтром если выбран район
        //    var query = _context.Meroprs
        //        .Include(m => m.UserMeroprs)
        //        .AsQueryable();

        //    if (!string.IsNullOrEmpty(SelectedDistrict) && SelectedDistrict != "Все")
        //    {
        //        query = query.Where(m => m.District == SelectedDistrict);
        //    }

        //    Meroprs = await query.ToListAsync();
        //}

        public async Task OnGetAsync()
        {
            Districts = await _context.Meroprs
                .Select(m => m.District)
                .Distinct()
                .ToListAsync();

            var query = _context.Meroprs
                .Include(m => m.UserMeroprs)
                .AsQueryable();

            // ? 1. ФИЛЬТР ПО РАЙОНУ (НЕ ТРОГАЕМ)
            if (!string.IsNullOrEmpty(SelectedDistrict) && SelectedDistrict != "Все")
            {
                query = query.Where(m => m.District == SelectedDistrict);
            }

            var list = await query.ToListAsync();

            var today = DateTime.Today;

            // ? 2. ФИЛЬТР ПО ДАТЕ / СТАТУСУ
            if (!string.IsNullOrEmpty(SelectedTimeFilter) && SelectedTimeFilter != "Все")
            {
                list = list.Where(m =>
                {
                    var date = m.EventDate?.Date;

                    int diff = date.HasValue ? (date.Value - today).Days : int.MaxValue;

                    return SelectedTimeFilter switch
                    {
                        "Сегодня" => date.HasValue && diff == 0,
                        "Завтра" => date.HasValue && diff == 1,
                        "7дней" => date.HasValue && diff > 1 && diff <= 7,
                        "Скоро" => date.HasValue && diff > 7,
                        "Прошло" => date.HasValue && diff < 0,

                        "По дням недели" => !m.EventDate.HasValue && !string.IsNullOrEmpty(m.DayOfWeek),

                        // fallback для мероприятий без даты (DayOfWeek)
                        "ДниНедели" => !m.EventDate.HasValue,

                        _ => true
                    };
                }).ToList();
            }

            Meroprs = list;
        }

        public async Task<IActionResult> OnPostRegisterAsync(int meroprId)
        {
            var userId = int.Parse(User.Claims.First(c => c.Type == "UserId").Value);

            

            // Проверка: уже записан?
            bool exists = await _context.UserMeroprs
                .AnyAsync(um => um.UserId == userId && um.MeroprId == meroprId);

            if (exists)
            {
                Message = "Вы уже записаны на это мероприятие!";
                return RedirectToPage();
            }

            // Проверка свободных мест
            var meropr = await _context.Meroprs
                .Include(m => m.UserMeroprs)
                .FirstOrDefaultAsync(m => m.Id == meroprId);

            if (meropr == null) ////новое
            {
                Message = "Мероприятие не найдено!";
                return RedirectToPage();
            }

            if (meropr.EventDate.HasValue && meropr.EventDate.Value.Date < DateTime.Today)
            {
                Message = "Нельзя записаться на прошедшее мероприятие!";
                return RedirectToPage();
            } //

            int taken = meropr.UserMeroprs.Count;

            if (taken >= meropr.MaxParticipants)
            {
                Message = "Нет свободных мест!";
                return RedirectToPage();
            }

            // Создаем запись
            var registration = new UserMeropr
            {
                UserId = userId,
                MeroprId = meroprId,
                /*RegisteredAt = DateTime.Now*/  // <-- добавляем текущую дату
                RegisteredAt = DateTime.UtcNow
            };

            _context.UserMeroprs.Add(registration);
            await _context.SaveChangesAsync();

            Message = "Вы успешно записались!";
            return RedirectToPage();

            
        }







    }
}
