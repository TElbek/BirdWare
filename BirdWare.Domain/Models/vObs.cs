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

        public static VObs MapToVObs(Observation o, 
                                     Fugletur f, 
                                     Lokalitet l, 
                                     Region r, 
                                     Art a, 
                                     Gruppe g, 
                                     Familie fa)
        {
            return new VObs
            {
                Aarstal = f.Aarstal,
                ArtId = a.Id,
                ArtNavn = a.Navn ?? string.Empty,
                Bem = o.Beskrivelse ?? string.Empty,
                Dato = f.Dato,
                FamilieId = fa.Id,
                FamilieNavn = fa.Navn ?? string.Empty,
                FugleturId = f.Id,
                GruppeId = g.Id,
                GruppeNavn = g.Navn ?? string.Empty,
                Latitude = l.Latitude,
                Longitude = l.Longitude,
                LokalitetId = l.Id,
                LokalitetNavn = l.Navn ?? string.Empty,
                Maaned = f.Maaned,
                ObservationId = o.Id,
                RegionId = r.Id,
                RegionNavn = r.Navn ?? string.Empty,
                Speciel = a.Speciel,
                SU = a.SU
            };
        }
    }
}