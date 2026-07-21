namespace BirdWare.Domain.Models
{
    public class FugleturAnalyseData
    {
        public long ArtId { get; set; }
        public long LokalitetId { get; set; }
        public long KommuneId { get; set; }
        public long RegionId { get; set; }
        public DateTime? Dato { get; set; }
    }
}
