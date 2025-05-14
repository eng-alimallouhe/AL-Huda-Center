using LMS.Common.Enums;

namespace LMS.Common.Interfaces
{
    public interface IEmailTemplateReaderService
    {
        public string? ReadTemplate(SupportedLanguages language, EmailPurpose purpose);
    }
}
