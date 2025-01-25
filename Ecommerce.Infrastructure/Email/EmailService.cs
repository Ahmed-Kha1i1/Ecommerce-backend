using Ecommerce.Application.Contracts;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace Ecommerce.Infrastructure.Email
{
    public class EmailService : IEmailService
    {
        private readonly SmtpSettings _stmpSettings;

        public EmailService(IOptions<SmtpSettings> stmpSettings)
        {
            _stmpSettings = stmpSettings.Value;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body, bool isHtmlMessage = true)
        {
            try
            {
                using (var client = new SmtpClient(_stmpSettings.Host, _stmpSettings.Port)
                {
                    Credentials = new NetworkCredential(_stmpSettings.Username, _stmpSettings.Password),
                    EnableSsl = true
                })
                {
                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(_stmpSettings.FromEmail, _stmpSettings.FromName),
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = isHtmlMessage,
                    };
                    mailMessage.To.Add(toEmail);

                    await client.SendMailAsync(mailMessage);
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error sending email.");
                throw;
            }
        }
    }
}
