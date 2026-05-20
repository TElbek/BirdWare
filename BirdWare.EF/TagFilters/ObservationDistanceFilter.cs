using BirdWare.Domain.Entities;
using BirdWare.Domain.Models;
using NetTopologySuite.Geometries;

namespace BirdWare.EF.TagFilters
{
    public class ObservationDistanceFilter : ObservationTagFilter
    {
        private const double VSOPLatitude = 12.4654394049127;
        private const double VSOPLongitude = 55.8025850215902;
        private const double metersPerKilometer = 1000;
        private const int Srid = 4326;

        public override IQueryable<Observation> Filter(List<Tag> tagList, IQueryable<Observation> queryable)
        {
            var distanceTag = tagList
                                .Where(t => t.TagType == TagTypes.Distance)
                                .OrderBy(t => t.SomeValue)
                                .FirstOrDefault() ?? new Tag { SomeValue = 0 };

            return queryable
                    .Where(o => o.Fugletur.Lokalitet.Point != null && 
                                o.Fugletur.Lokalitet.Point.Distance(GetVSOPPOI()) <= distanceTag.SomeValue * metersPerKilometer);
        }

        private static Point GetVSOPPOI()
        {
            var geometryFactory = new GeometryFactory(new PrecisionModel(), Srid);
            return geometryFactory.CreatePoint(new Coordinate(VSOPLatitude, VSOPLongitude));
        }
    }
}
