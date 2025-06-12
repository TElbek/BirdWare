namespace BirdWare.Domain.Models
{
    public class VObs
    {
        public long ObservationId { get; set; }
        public string GruppeNavn { get; set; } = string.Empty;

        public long GruppeId { get; set; }

        public string ArtNavn { get; set; } = string.Empty;

        public long ArtId { get; set; }

        public string Bem { get; set; } = string.Empty;

        public DateTime? Dato { get; set; }

        public string Generelt { get; set; } = string.Empty;

        public long LokalitetId { get; set; }

        public string LokalitetNavn { get; set; } = string.Empty;

        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public long FugleturId { get; set; }

        public long FamilieId { get; set; }

        public string FamilieNavn { get; set; } = string.Empty;

        public long RegionId { get; set; }

        public string RegionNavn { get; set; } = string.Empty;

        public bool SU { get; set; }
        public bool Speciel { get; set; }

        public long Aarstal {  get; set; }
        public long Maaned {  get; set; }
    }
}