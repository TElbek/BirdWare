namespace BirdWare.Domain.Utilities
{
    internal class StringOperations
    {
        public static string ToTitleCase(string value)
        {
            var textInfo = CultureProvider.GetCulture().TextInfo;
            return textInfo.ToTitleCase(value);
        }
    }
}
