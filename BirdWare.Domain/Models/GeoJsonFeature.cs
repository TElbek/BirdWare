using System.Dynamic;

namespace BirdWare.Domain.Models
{
    public class GeoJsonFeature
    {
        public string Type { get; set; } = "Feature";
        public ExpandoObject Properties { get; set; }
        public GeoJsonGeometryPoint Geometry { get; set; }

        public GeoJsonFeature() 
        { 
            Properties = new ExpandoObject();
            Geometry = new GeoJsonGeometryPoint();
        }
    }
}
