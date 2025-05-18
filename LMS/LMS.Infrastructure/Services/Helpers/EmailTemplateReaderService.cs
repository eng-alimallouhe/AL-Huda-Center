using LMS.Application.Abstractions.Services.EmailServices;
using LMS.Common.Enums;

namespace LMS.Infrastructure.Services.Helpers
{
    public class EmailTemplateReaderService : IEmailTemplateReaderService
    {
        public string? ReadTemplate(SupportedLanguages language, EmailPurpose purpose)
        {
            string templateString = string.Empty;


            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "EmailTemplates", language.ToString().ToUpper(), $"{purpose.ToString()}.html");

            if (!File.Exists(path))
            {
                return null;
            }

            templateString = File.ReadAllText(path);

            return templateString;
        }
    }
}
