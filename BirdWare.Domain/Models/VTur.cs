using System.Globalization;

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

        public string FugleturAarMaaned =>
            Dato.HasValue ?
            Aarstal.ToString() + " " + Dato.Value.ToString("MMMM", CultureInfo.GetCultureInfo("da-DK")) :
            string.Empty;
    }
}
