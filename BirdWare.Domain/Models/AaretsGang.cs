namespace BirdWare.Domain.Models
{
    public class AaretsGang
    {
        public long ArtId { get; set; }
        public string? ArtNavn { get; set; }
        public DateTime? Dato { get; set; }
        public string? FamilieNavn { get; set; }
        public long FugleturId { get; set; }
        public string? LokalitetNavn { get; set; }
        public bool SU { get; set; }

        public string Titel =>
            (Dato.HasValue ? Dato.Value.ToString("dd-MM-yyyy") : string.Empty) + " " + LokalitetNavn;
    }
}
