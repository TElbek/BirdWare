namespace BirdWare.Domain.Models
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AnalyseTypeAttribute(AnalyseTyper analyseType) : Attribute
    {
        public AnalyseTyper AnalyseType { get; set; } = analyseType;
    }
}
