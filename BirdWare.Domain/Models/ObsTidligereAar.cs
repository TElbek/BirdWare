namespace BirdWare.Domain.Models
{
    public class ObsTidligereAar
    {
        public long ArtId { get; set; }
        public string ArtNavn { get; set; } = string.Empty;
        public string GruppeNavn { get; set; } = string.Empty;
        public string FamilieNavn { get; set; } = string.Empty;
        public long LokalitetId { get; set; }
        public string LokalitetNavn { get; set; } = string.Empty;
        public double? Distance { get; set; }
    }
}
