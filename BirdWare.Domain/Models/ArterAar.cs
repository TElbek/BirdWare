namespace BirdWare.Domain.Models
{
    public class ArterAar
    {
        public string? ArtNavn { get; set; }
        public long FugleturId { get; set; }
        public long ArtId { get; set; }
        public DateTime? Dato { get; set; }
        public bool SU { get; set; }
        public bool Speciel { get; set; }
        public long GruppeId { get; set; }
    }
}
