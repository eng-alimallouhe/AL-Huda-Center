using LMS.Application.Abstractions.Services.EmailServices;
using LMS.Common.Enums;
using LMS.Domain.Enums.Commons;

namespace LMS.Infrastructure.Services.Helpers
{
    public class EmailTemplateReaderService : IEmailTemplateReaderService
    {
        public string? ReadTemplate(Language language, EmailPurpose purpose)
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

/*
 D:\\Library Management System\\LMS\\LMS.API\\bin\\Debug\\net9.0\\Resources\\EmailTemplates\\AR\\AccountVerificationTemplate.html
 */