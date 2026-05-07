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
            Credentials = new NetworkCredential("safe.spot.ua@gmail.com", "xers puso etgj euom"),
            EnableSsl = true
        };

        var message = new MailMessage("safe.spot.ua@gmail.com", to, subject, body);

        await smtp.SendMailAsync(message);
    }
}
