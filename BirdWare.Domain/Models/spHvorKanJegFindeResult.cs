namespace BirdWare.Domain.Models
{
    public class spHvorKanJegFindeResult
    {
        public long ArtId { get; set; }
        public string ArtNavn { get; set; } = string.Empty;
        public string LokalitetNavn { get; set; } = string.Empty;
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public double Distance { get; set; }
    }
}
