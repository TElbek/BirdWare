namespace BirdWare.Domain.Models
{
    public class HvorKanJegFindeResponse
    {
        public long ArtId { get; set; }
        public long AntalObs { get; set; }
        public string ArtNavn { get; set; } = string.Empty;
        public string GruppeNavn { get; set; } = string.Empty;
        public string FamilieNavn { get; set; } = string.Empty;
        public string LokalitetNavn { get; set; } = string.Empty;
        public long LokalitetId { get; set; }
        public double? Distance { get; set; }
    }
}
