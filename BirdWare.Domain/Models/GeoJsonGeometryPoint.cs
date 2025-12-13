namespace BirdWare.Domain.Models
{
    public class GeoJsonGeometryPoint
    {
        public string Type { get; set; } = "Point";
        public double[] Coordinates { get; set; } = [0,0];
    }
}