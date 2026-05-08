using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SeniorCenterWebApp.Data;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SeniorCenterWebApp.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly DataContext _context;
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(DataContext context, ILogger<LoginModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            public string Username { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        public void OnGet()
        {
            _logger.LogInformation("Приложение успешно запущено.");
            _logger.LogInformation("Подключение к базе данных установлено с использованием безопасного хранения UserSecret.");
            _logger.LogInformation("Страница входа загружена.");
            _logger.LogInformation("Страница логина загружена");
            //_logger.LogInformation("Резервная копия SQL Server создана: C:\\SQLBackups\\SeniorCenterDB_20260317_1530.bak");
            //_logger.LogInformation("Резервная копия скопирована в проект: .\\Backup\\SeniorCenterDB_20260317_1530.bak");
            //_logger.LogInformation("База данных восстановлена из файла: C:\\SQLBackups\\SeniorCenterDB_20260317_1530.bak");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var stopwatch = Stopwatch.StartNew(); //лог

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Модель логина недействительна");
                return Page();
            }

            // Ищем пользователя по Username
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == Input.Username);

            if (user == null)
            {
                _logger.LogWarning("Попытка входа с несуществующим пользователем: {Username}", Input.Username);
                ModelState.AddModelError(string.Empty, "Неверное имя пользователя или пароль.");
                return Page();
            }

            // Проверка пароля с солью
            if (!VerifyPassword(Input.Password, user.PasswordHash, user.Salt))
            {
                _logger.LogWarning("Неверный пароль для пользователя: {Username}", Input.Username);
                ModelState.AddModelError(string.Empty, "Неверное имя пользователя или пароль.");
                return Page();
            }

            // Создаем claims для аутентификации
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim("FullName", user.FullName ?? ""),
                new Claim("UserId", user.Id.ToString()) // <- добавляем UserId
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7)
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            

            _logger.LogInformation("Пользователь {Username} успешно вошел в систему", Input.Username);

            // нагрузки
            await Task.Delay(Random.Shared.Next(10, 40));

            stopwatch.Stop();

            _logger.LogInformation(
                "User '{Username}' logged in {Time} ms",
                Input.Username,
                stopwatch.ElapsedMilliseconds
            );

            // Перенаправление в зависимости от роли с логированием
            if (user.Role == "User")
            {
                _logger.LogInformation(
                    "Пользователь '{Username}' был перенаправлен на страницу CenterInfo",
                    Input.Username
                );
                return RedirectToPage("/Account/CenterInfo");
            }
            else
            {
                _logger.LogInformation(
                    "Пользователь '{Username}' был перенаправлен на страницу Admin",
                    Input.Username
                );
                return RedirectToPage("/Account/Admin");
            }



        }

        //проверка пароля
        private bool VerifyPassword(string password, string storedHash, string storedSalt)
        {
            using var sha256 = SHA256.Create();
            var combined = Encoding.UTF8.GetBytes(password + storedSalt);
            var hash = sha256.ComputeHash(combined);
            var hashString = Convert.ToBase64String(hash);

            return hashString == storedHash;
        }
    }
}
