using NetTopologySuite.Geometries;

namespace BirdWare.EF.Geography
{
    internal static class GeographyPoint
    {
        private const int Srid = 4326;

        public const double VSOPLatitude = 55.8025850215902;
        public const double VSOPLongitude = 12.4654394049127;
        public const double metersPerKilometer = 1000;

        public static Point GetPointFromLatLong(double latitude, double longitude)
        {
            var geometryFactory = new GeometryFactory(new PrecisionModel(), Srid);
            //Coordinates in NTS are in terms of X and Y values.
            //To represent longitude and latitude, use X for longitude and Y for latitude.
            //Note that this is backwards from the latitude, longitude format in which you typically see these values.
            //https://learn.microsoft.com/en-us/ef/core/modeling/spatial
            return geometryFactory.CreatePoint(new Coordinate(longitude, latitude));
        }
    }
}