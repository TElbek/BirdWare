using BirdWare.Domain.Entities;

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

        public static VObs MapFromObservation(Observation m)
        {
            return new VObs()
            {
                ObservationId = m.Id,
                Dato = m.Fugletur.Dato,
                ArtId = m.Art.Id,
                FamilieId = m.Art.Gruppe.FamilieId,
                FugleturId = m.Fugletur.Id,
                ArtNavn = m.Art.Navn ?? string.Empty,
                FamilieNavn = m.Art.Gruppe.Familie.Navn ?? string.Empty,
                GruppeNavn = m.Art.Gruppe.Navn ?? string.Empty,
                GruppeId = m.Art.GruppeId,
                SU = m.Art.SU,
                Speciel = m.Art.Speciel,
                LokalitetNavn = m.Fugletur.Lokalitet.Navn ?? string.Empty,
                LokalitetId = m.Fugletur.Lokalitet.Id,
                Latitude = m.Fugletur.Lokalitet.Latitude,
                Longitude = m.Fugletur.Lokalitet.Longitude,
                Bem = m.Beskrivelse ?? string.Empty,
                RegionId = m.Fugletur.Lokalitet.RegionId,
                RegionNavn = m.Fugletur.Lokalitet.Region.Navn ?? string.Empty,
                Aarstal = m.Fugletur.Aarstal,
                Maaned = m.Fugletur.Maaned
            };
        }
    }
}