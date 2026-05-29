using System.ComponentModel;

namespace BirdWare.Domain.Models
{
    public enum AnalyseTyper
    {
        [Description("1. Obs Birdware.dk")]
        FoersteObsIDatabasen = 1,

        [Description("1. Obs Danmark")]
        FoersteObsIDK = 2,
        
        [Description("1. Obs [Region]")]
        FoersteObsIRegion = 3,

        [Description("1. Obs [Kommune]")]
        FoersteObsForKommune = 4,

        [Description("1. Obs [Lokalitet]")]
        FoersteObsForLokalitet = 5, //4
        
        [Description("1. Obs [Aar]")]
        FoersteObsIAar = 6, //5
        
        [Description("1. Obs [Maaned]")]
        FoersteObsIMaaned = 7 //6        
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
        public bool Speciel { get; set; }
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
