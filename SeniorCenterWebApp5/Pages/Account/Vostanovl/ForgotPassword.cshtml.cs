using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SeniorCenterWebApp.Data;
using SeniorCenterWebApp.Services;
using System.ComponentModel.DataAnnotations;

namespace SeniorCenterWebApp.Pages.Account.Vostanovl
{
    public class ForgotPasswordModel : PageModel
    {
        private readonly DataContext _context;
        private readonly PasswordResetService _resetService;
        private readonly IEmailSender _emailSender;

        public ForgotPasswordModel(
            DataContext context,
            PasswordResetService resetService,
            IEmailSender emailSender)
        {
            _context = context;
            _resetService = resetService;
            _emailSender = emailSender;
        }

        [BindProperty]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var user = await _context.Users
                //.FirstOrDefaultAsync(u => u.Email == Email);
                .FirstOrDefaultAsync(u => u.Email != null && u.Email == Email);

            // ?? ВАЖНО: не показываем, существует ли email
            if (user != null)
            {
                // можно добавить логирование или задержку
                await Task.Delay(500);


                var token = _resetService.GenerateToken(user.Email);

                var resetLink = Url.Page(
                     //"/Account/ResetPassword",
                     "/Account/Vostanovl/ResetPassword",
                    null,
                    new { token },
                    Request.Scheme);

                await _emailSender.SendEmailAsync(
                    Email,
                    "Сброс пароля",
                    $"Перейдите по ссылке для сброса пароля:<br><a href='{resetLink}'>Сбросить пароль</a>");
            }

            return RedirectToPage("ForgotPasswordConfirmation");
        }
    }
}
