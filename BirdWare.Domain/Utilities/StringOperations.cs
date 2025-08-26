using System.Globalization;

namespace BirdWare.Domain.Utilities
{
    internal class StringOperations
    {
        public static string ToTitleCase(string value)
        {
            var textInfo = new CultureInfo("en-US", false).TextInfo;
            return textInfo.ToTitleCase(value);
        }
    }
}
