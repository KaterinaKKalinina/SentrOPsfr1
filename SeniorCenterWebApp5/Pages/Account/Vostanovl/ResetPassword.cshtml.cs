using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SeniorCenterWebApp.Data;
using SeniorCenterWebApp.Services;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace SeniorCenterWebApp.Pages.Account.Vostanovl
{
    public class ResetPasswordModel : PageModel
    {
        private readonly DataContext _context;
        private readonly PasswordResetService _resetService;

        public ResetPasswordModel(
            DataContext context,
            PasswordResetService resetService)
        {
            _context = context;
            _resetService = resetService;
        }

        [BindProperty]
        public string Token { get; set; }

        //новое
        [BindProperty]
        [Required]
        public string ConfirmPassword { get; set; }

        [BindProperty]
        [Required]
        public string Password { get; set; }

        public void OnGet(string token)
        {
            Token = token;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var (email, date) = _resetService.ValidateToken(Token);

            // защита по времени (1 час)
            if ((DateTime.UtcNow - date).TotalHours > 1)
            {
                ModelState.AddModelError("", "—сылка устарела");
                return Page();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
                return RedirectToPage("ResetPasswordConfirmation");

            var salt = GenerateSalt();
            user.Salt = salt;
            user.PasswordHash = HashPassword(Password, salt);

            await _context.SaveChangesAsync();

            return RedirectToPage("ResetPasswordConfirmation");
        }

        private string GenerateSalt()
        {
            byte[] saltBytes = new byte[16];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(saltBytes);
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
