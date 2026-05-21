namespace BirdWare.Domain.Models
{
    public class LokaliteterByLatLong
    {
        private double distance;

        public long Id { get; set; }

        public double Distance
        {
            get => Math.Round(distance, 0);
            set => distance = value;
        }

        public string? Navn { get; set; } = string.Empty;
    }
}
