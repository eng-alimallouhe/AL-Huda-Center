using System.Security.Cryptography;
using LMS.Application.Abstractions.Services.Helpers;

namespace LMS.Infrastructure.Services.Helpers
{
    public class RandomGeneratorService : IRandomGeneratorService
    {
        public string GenerateSexDigitsCode()
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] randomBytes = new byte[6];
                rng.GetBytes(randomBytes);

                int code = BitConverter.ToInt32(randomBytes, 0) % 1000000;
                return Math.Abs(code).ToString("D6");
            }
        }
    }
}
