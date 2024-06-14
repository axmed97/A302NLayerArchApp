using Core.Utilities.Message.Abstract;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace Core.Utilities.Message.Concrete
{
    public class MessageService : IMessageService
    {
        private readonly IConfiguration _configuration;

        public MessageService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendMessage(string to, string subject, string message)
        {
            MailMessage mailMessage = new()
            {
                IsBodyHtml = true,
            };

            mailMessage.To.Add(to);
            mailMessage.Body = message;
            mailMessage.Subject = subject;
            mailMessage.From = new(_configuration["EmailSettings:Email"], "Axmed Axmedov", System.Text.Encoding.UTF8);

            SmtpClient smtpClient = new SmtpClient()
            {
                Port = Convert.ToInt32(_configuration["EmailSettings:Port"]),
                EnableSsl = true,
                Host = _configuration["EmailSettings:Host"],
                Credentials = new NetworkCredential(_configuration["EmailSettings:Email"], _configuration["EmailSettings:Password"])
            };

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
