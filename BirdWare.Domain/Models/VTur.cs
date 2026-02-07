using BirdWare.Domain.Entities;
using BirdWare.Domain.Utilities;

namespace BirdWare.Domain.Models
{
    public class VTur
    {
        public long Id { get; set; }
        public DateTime? Dato { get; set; }
        public long LokalitetId { get; set; }
        public string LokalitetNavn { get; set; } = string.Empty;
        public long RegionId { get; set; }
        public string RegionNavn { get; set; } = string.Empty;
        public long Aarstal { get; set; }
        public long Maaned { get; set; }
        public long AntalArter { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public string FugleturAarMaaned =>
            Dato.HasValue ?
            Aarstal.ToString() + " " + DateFormatting.GetMonthName(Dato.Value) :
            string.Empty;

        public static VTur MapFromFugletur(Fugletur s)
        {
            return new VTur
            {
                Id = s.Id,
                Dato = s.Dato,
                LokalitetId = s.LokalitetId,
                LokalitetNavn = s.Lokalitet?.Navn ?? string.Empty,
                RegionId = s.Lokalitet?.RegionId ?? 0,
                RegionNavn = s.Lokalitet?.Region?.Navn ?? string.Empty,
                Aarstal = s.Aarstal,
                Maaned = s.Maaned,
                Latitude = s.Lokalitet?.Latitude,
                Longitude = s.Lokalitet?.Longitude
            };
        }
    }
}
