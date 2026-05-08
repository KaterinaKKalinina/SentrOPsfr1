using MailKit.Net.Smtp;
using MimeKit;

namespace SeniorCenterWebApp.Services
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress("SeniorCenter", "katyakd20@mail.ru"));
            message.To.Add(MailboxAddress.Parse(email));
            message.Subject = subject;

            message.Body = new TextPart("html")
            {
                Text = htmlMessage
            };

            using var client = new SmtpClient();

            await client.ConnectAsync(
                "smtp.mail.ru",
                465,
                MailKit.Security.SecureSocketOptions.SslOnConnect);

            await client.AuthenticateAsync(
                "katyakd20@mail.ru",
                "g0sQJW2EKF06ZCkW97bz");

            await client.SendAsync(message);

            await client.DisconnectAsync(true);
        }
    }
}