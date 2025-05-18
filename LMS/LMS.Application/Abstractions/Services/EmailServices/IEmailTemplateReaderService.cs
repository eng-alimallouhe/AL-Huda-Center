using LMS.Common.Enums;

namespace LMS.Application.Abstractions.Services.EmailServices
{
    public interface IEmailTemplateReaderService
    {
        public string? ReadTemplate(SupportedLanguages language, EmailPurpose purpose);
    }
}
