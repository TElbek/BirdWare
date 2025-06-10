namespace BirdWare.Domain.Models
{
    public class ByLatitudeLongitude
    {
        public long Id { get; set; }
        public string? Navn { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public long Count { get; set; }
    }
}
