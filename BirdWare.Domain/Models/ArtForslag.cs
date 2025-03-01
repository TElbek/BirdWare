namespace BirdWare.Domain.Models
{
    public class ArtForslag
    {
        public long ArtId { get; set; }
        public string ArtNavn { get; set; } = string.Empty;
        public string FamilieNavn { get; set; } = string.Empty;
        public long Indeks { get; set; }
    }
}
