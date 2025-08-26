using BirdWare.Domain.Utilities;

namespace BirdWare.Domain.Models
{
    public class Forskel
    {
        public long ArtId { get; set; }
        public string? ArtNavn { get; set; }
        public DateTime? Dato { get; set; }
        public long RegionId { get; set; }
        public bool SU { get; set; }
        public bool Speciel { get; set; }
        public long LokalitetId { get; set; }
        public string? LokalitetNavn { get; set; }
        public string Titel => 
            (Dato.HasValue ? DateFormatting.GetDatoFormateret(Dato.Value) : string.Empty) + " " + LokalitetNavn;

        public string? FamilieNavn { get; set; }
    }
}
