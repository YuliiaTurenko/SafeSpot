using SafeSpot.Application.Abstractions;
using System.Net;
using System.Net.Mail;

namespace SafeSpot.Infrastructure.Services;

public class EmailService : IEmailService
{
    public async Task SendAsync(string to, string subject, string body)
    {
        var smtp = new SmtpClient("smtp.gmail.com", 587)
        {
            Credentials = new NetworkCredential("uliaturenko604@gmail.com", "qzny ugvv bgup zvyd"),
            EnableSsl = true
        };

        var message = new MailMessage("uliaturenko604@gmail.com", to, subject, body);

        await smtp.SendMailAsync(message);
    }
}
