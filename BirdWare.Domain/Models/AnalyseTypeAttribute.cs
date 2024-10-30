namespace BirdWare.Domain.Models
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AnalyseTypeAttribute(AnalyseTyper analyseType, bool kunIndland) : Attribute
    {
        public AnalyseTyper AnalyseType { get; set; } = analyseType;
        public bool KunIndland { get; set; } = kunIndland;
    }
}
