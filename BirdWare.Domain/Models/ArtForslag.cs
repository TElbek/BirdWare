namespace BirdWare.Domain.Models
{
    public class ArtForslag
    {
        public long Id { get; set; }
        public string Navn { get; set; } = string.Empty;
        public long AntalObs { get; set; }
    }
}
