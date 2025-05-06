using LMS.Domain.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace LMS.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly string _fromEmail;
        private readonly string _password;
        private readonly string _host;
        private readonly int _port;

        public EmailService(string email, string password, string host, int port)
        {
            _fromEmail = email;
            _password = password;
            _host = host;
            _port = port;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            var mail = new MimeMessage();
            mail.From.Add(MailboxAddress.Parse(_fromEmail));
            mail.To.Add(MailboxAddress.Parse(toEmail));

            mail.Subject = subject;
            mail.Body = new TextPart(TextFormat.Html) { Text = message };

            var smtp = new SmtpClient();

            await smtp.ConnectAsync(_host, _port, SecureSocketOptions.StartTls);

            await smtp.AuthenticateAsync(_fromEmail, _password);

            await smtp.SendAsync(mail);

            await smtp.DisconnectAsync(true);
        }
    }
}
