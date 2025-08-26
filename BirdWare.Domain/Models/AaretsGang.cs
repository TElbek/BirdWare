using BirdWare.Domain.Utilities;

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
        public bool Speciel { get; set; }
        public string? Maaned => 
            StringOperations.ToTitleCase((Dato.HasValue ? DateFormatting.GetMonthName(Dato.Value) : string.Empty));

        public string Titel =>
            (Dato.HasValue ? DateFormatting.GetDatoFormateret(Dato.Value) : string.Empty) + " " + LokalitetNavn;
    }
}
