namespace LMS.Application.Abstractions.Services.EmailSender
{
    public interface IEmailSenderService
    {
        Task SendEmailAsync(string toEmail, string subject, string message);
    }
}