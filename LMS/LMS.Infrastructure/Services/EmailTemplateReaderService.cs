using LMS.Common.Enums;
using LMS.Common.Interfaces;

namespace LMS.Infrastructure.Services
{
    public class EmailTemplateReaderService : IEmailTemplateReaderService
    {
        public string? ReadTemplate(SupportedLanguages language, EmailPurpose purpose)
        {
            string templateString = string.Empty;


            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EmailTemplates", language.ToString().ToUpper(), $"{purpose.ToString()}.html");

            if (!File.Exists(path))
            {
                return null;
            }

            templateString = File.ReadAllText(path);

            return templateString;
        }
    }
}
