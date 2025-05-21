using LMS.Common.Enums;
using LMS.Domain.Enums.Commons;

namespace LMS.Application.Abstractions.Services.EmailServices
{
    public interface IEmailTemplateReaderService
    {
        public string? ReadTemplate(Language language, EmailPurpose purpose);
    }
}
