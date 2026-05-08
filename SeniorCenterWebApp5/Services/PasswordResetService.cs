using Microsoft.AspNetCore.DataProtection;
using System.Text;

namespace SeniorCenterWebApp.Services
{
    public class PasswordResetService
    {
        private readonly IDataProtector _protector;

        public PasswordResetService(IDataProtectionProvider provider)
        {
            _protector = provider.CreateProtector("PasswordReset");
        }

       
        public string GenerateToken(string email)
        {
            var data = $"{email}|{DateTime.UtcNow}";
            var protectedData = _protector.Protect(data);

            return Convert.ToBase64String(Encoding.UTF8.GetBytes(protectedData))
                .Replace("+", "-")
                .Replace("/", "_")
                .Replace("=", "");
        }

       
        public (string email, DateTime date) ValidateToken(string token)
        {
            token = token.Replace("-", "+").Replace("_", "/");

            switch (token.Length % 4)
            {
                case 2: token += "=="; break;
                case 3: token += "="; break;
            }

            var decoded = Encoding.UTF8.GetString(Convert.FromBase64String(token));
            var unprotected = _protector.Unprotect(decoded);

            var parts = unprotected.Split('|');

            return (parts[0], DateTime.Parse(parts[1]));
        }

    }
}
