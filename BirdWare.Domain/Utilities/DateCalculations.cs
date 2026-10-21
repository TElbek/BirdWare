namespace BirdWare.Domain.Utilities
{
    public class DateCalculations
    {
        private const int TrediveDage = 30;
        public const int TrediveDageTidligere = TrediveDage * -1;
        public const int OmTrediveDage = TrediveDage;

        public static int GetRelativDagIAaret(int days)
        {
            if (days > 0)
            {
                if (DateTime.Now.AddDays(days).Year > DateTime.Now.Year)
                {
                    return DateTime.IsLeapYear(DateTime.Now.Year) ? 366 : 365;
                }
                return DateTime.Now.DayOfYear + days;
            }
            if (days < 0)
            {
                if (DateTime.Now.AddDays(days).Year < DateTime.Now.Year)
                {
                    return 1;
                }
                return DateTime.Now.DayOfYear - Math.Abs(days);
            }
            return DateTime.Now.DayOfYear;
        }
    }
}