using System.Globalization;

namespace BirdWare.Domain.Utilities
{
    internal class CultureProvider
    {
        public static CultureInfo GetCulture() => new("da-DK");
    }
}
