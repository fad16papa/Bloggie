using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Bloggie.Web.Repositories.Services
{
    public class EmailSenderService : IEmailSender
    {
        private readonly IConfiguration _configuration;
        public EmailSenderService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {
                var smtpServer = _configuration["EmailSettings:SmtpServer"];
                var smtpPort = int.Parse(_configuration["EmailSettings:SmtpPort"]);
                var username = _configuration["EmailSettings:Username"];
                var password = _configuration["EmailSettings:Password"];

                using (var client = new SmtpClient(smtpServer, smtpPort))
                {
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(username, password);
                    client.EnableSsl = true;

                    var message = new MailMessage
                    {
                        From = new MailAddress(username),
                        Subject = subject,
                        Body = htmlMessage,
                        IsBodyHtml = true
                    };

                    message.To.Add(email);

                    await client.SendMailAsync(message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SMTP Error: {ex.Message}");
                Console.WriteLine($"Server Response: {ex.InnerException}");
            }

        }
    }
}