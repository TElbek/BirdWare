namespace BirdWare.Domain.Models
{
    public class GeoJsonFeature
    {
        public string Type { get; set; } = "Feature";
        public GeoJsonFeatureProperties Properties { get; set; }
        public GeoJsonGeometryPoint Geometry { get; set; }

        public GeoJsonFeature() 
        { 
            Properties = new GeoJsonFeatureProperties();
            Geometry = new GeoJsonGeometryPoint();
        }
    }
}
