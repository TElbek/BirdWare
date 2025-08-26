namespace BirdWare.Domain.Utilities
{
    internal class DateFormatting
    {
        public static string GetDatoFormateret(DateTime dateTime)
        {
            return dateTime.ToString("dd-MM-yyyy", CultureProvider.GetCulture());
        }

        public static string GetMonthName(DateTime dateTime)
        {
            return dateTime.ToString("MMMM", CultureProvider.GetCulture());
        }
    }
}
