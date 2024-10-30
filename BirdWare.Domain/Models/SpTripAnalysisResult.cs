namespace BirdWare.Domain.Models
{
    public class SpTripAnalysisResult
    {
        public AnalyseTyper AnalyseType { get; set; }
        public int ArtId { get; set; }
        public bool SU { get; set; }
        public string ArtNavn { get; set; } = string.Empty;
    }
}
