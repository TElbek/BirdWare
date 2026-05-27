using NetTopologySuite.Geometries;

namespace BirdWare.Domain.Entities
{
    public class Kommune
    {
        public long Id { get; set; }
        public long RegionId { get; set; }
        public string Navn { get; set; } = string.Empty;
        public Point? Point0 { get; set; }
        public Point? Point1 { get; set; }

        public virtual Region? Region { get; set; }
    }
}
