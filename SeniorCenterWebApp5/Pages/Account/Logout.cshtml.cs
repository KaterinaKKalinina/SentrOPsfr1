using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;


//namespace SeniorCenterWebApp.Pages.Account
//{
//    [Authorize] // Требуется авторизация для выхода
//    public class LogoutModel : PageModel
//    {
//        public async Task<IActionResult> OnGet()
//        {
//            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
//            return RedirectToPage("/Index"); // Перенаправление на главную страницу
//        }
//    }
//}



namespace SeniorCenterWebApp.Pages.Account
{
    [Authorize] // Доступ только для авторизованных
    public class LogoutModel : PageModel
    {
        // Свойство для передачи статуса в вид
        public bool IsLoggedOut { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            // 1. Выход из всех схем аутентификации
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme
            );

            // Если используете другие схемы (JWT, OpenID Connect и т.д.), добавьте:
            // await HttpContext.SignOutAsync("YourSchemeName");

            // 2. Очистка сессии (если используется)
            HttpContext.Session.Clear();

            // 3. Удаление куки
            HttpContext.Response.Cookies.Delete(".AspNetCore.Session");
            HttpContext.Response.Cookies.Delete(".AspNetCore.Identity.Application");

            // 4. Установка HTTP-заголовков против кэширования
            Response.Headers.Append("Cache-Control", "no-store, no-cache, must-revalidate, max-age=0");
            Response.Headers.Append("Pragma", "no-cache");
            Response.Headers.Append("Expires", "0");

            // 5. Установка флага успешного выхода
            IsLoggedOut = true;

            // 6. Перенаправление на главную (можно оставить как OnGet, без Redirect)
            return Page(); // Отдаём ту же страницу с обновлённым IsLoggedOut
        }
    }
}