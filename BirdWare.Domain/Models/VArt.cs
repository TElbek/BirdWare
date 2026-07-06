namespace BirdWare.Domain.Models
{
    public class VArt
    {
        public string GruppeNavn { get; set; } = string.Empty;

        public long GruppeId { get; set; }

        public string ArtNavn { get; set; } = string.Empty;

        public long ArtId { get; set; }

        public long FamilieId { get; set; }

        public string FamilieNavn { get; set; } = string.Empty;

        public bool SU { get; set; }
        public bool Speciel { get; set; }
    }
}
