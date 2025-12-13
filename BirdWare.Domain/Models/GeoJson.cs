namespace BirdWare.Domain.Models
{
    public class GeoJson
    {
        public string Type { get; set; } = "FeatureCollection";
        public string Name { get; set; } = "Observation";
        public GeoJsonCRS CRS { get; set; }
        public List<GeoJsonFeature> Features { get; set; }

        public GeoJson() 
        {
            CRS = new GeoJsonCRS();
            Features = [];
        }
    }
}
