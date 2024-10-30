using System.ComponentModel;

namespace BirdWare.Domain.Models
{
    public enum AnalyseTyper
    {
        [Description("1. Obs i Databasen")]
        FoersteObsIDatabasen = 1,
        [Description("1. Obs i DK")]
        FoersteObsIDK = 2,
        [Description("1. Obs i [Region]")]
        FoersteObsIRegion = 3,
        [Description("1. Obs for [Lokalitet]")]
        FoersteObsForLokalitet = 4,
        [Description("1. Obs i [Aar]")]
        FoersteObsIAar = 5,
        [Description("1. Obs i [Maaned]")]
        FoersteObsIMaaned = 6,
    }

    public class AnalyseTypeModel
    {
        public AnalyseTyper AnalyseType { get; set; }
        public string AnalyseTypeTekst => AnalyseType.GetDescription();
    }

    public class FugleturAnalyse
    {
        public AnalyseTyper AnalyseType { get; set; }
        public string AnalyseTypeTekst => AnalyseType.GetDescription();
        public long ArtId { get; set; }
        public string ArtNavn { get; set; } = string.Empty;
        public long LokalitetId { get; set; }
        public bool SU { get; set; }
    }

    public static class AnalyseExtensions
    {
        public static string GetDescription(this AnalyseTyper analyseType)
        {
            var fieldInfo = analyseType.GetType().GetField(analyseType.ToString());
            if (fieldInfo != null)
            {
                object[] descriptionAttrs = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (descriptionAttrs.Length > 0)
                {
                    DescriptionAttribute description = (DescriptionAttribute)descriptionAttrs[0];
                    return description.Description;
                }
            }
            return analyseType.ToString();
        }
    }
}
