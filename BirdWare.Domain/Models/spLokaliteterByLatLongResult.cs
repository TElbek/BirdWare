namespace BirdWare.Domain.Models
{
    public class spLokaliteterByLatLongResult
    {
        public long Id { get; set; }
        public double Distance { get; set; }
        public string Navn { get; set; } = string.Empty;
        public int AntalTure { get; set; }
    }
}
