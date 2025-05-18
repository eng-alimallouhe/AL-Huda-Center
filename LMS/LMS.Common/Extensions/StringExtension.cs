namespace LMS.Common.Extensions
{
    public static class StringExtension
    {
        public static string ToNormalize(this string value)
        {
            return value.ToUpperInvariant().Trim() ?? string.Empty;
        }
    }
}
