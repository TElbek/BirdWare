using NetTopologySuite.Geometries;

namespace BirdWare.EF.Geography
{
    internal static class GeographyPointFactory
    {
        private const int Srid = 4326;

        public static Point GetPointFromLatLong(double latitude, double longitude)
        {
            var geometryFactory = new GeometryFactory(new PrecisionModel(), Srid);
            return geometryFactory.CreatePoint(new Coordinate(latitude, longitude));
        }

    }
}
