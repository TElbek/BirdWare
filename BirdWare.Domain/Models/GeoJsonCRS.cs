namespace BirdWare.Domain.Models
{
    public class GeoJsonCRS
    {
        public string Type { get; set; } = "name";
        public GeoJsonCRSProperties Properties { get; set; }

        public GeoJsonCRS()
        {
            Properties = new GeoJsonCRSProperties();
        }
    }
}