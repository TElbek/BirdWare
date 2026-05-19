using BirdWare.Domain.Entities;
using BirdWare.Domain.Models;
using NetTopologySuite.Geometries;

namespace BirdWare.EF.TagFilters
{
    public class ObservationDistanceFilter : ObservationTagFilter
    {
        private const double VSOPLatitude = 12.4654394049127;
        private const double VSOPLongitude = 55.8025850215902;
        private const int Srid = 4326;

        public override IQueryable<Observation> Filter(List<Tag> tagList, IQueryable<Observation> queryable)
        {
            return queryable;
        }

        private Point GetVSOPPOI()
        {
            var geometryFactory = new GeometryFactory(new PrecisionModel(), Srid);
            return geometryFactory.CreatePoint(new Coordinate(VSOPLatitude, VSOPLongitude));
        }
    }
}
