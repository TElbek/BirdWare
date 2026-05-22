using BirdWare.Domain.Entities;
using BirdWare.Domain.Models;
using BirdWare.EF.Geography;

namespace BirdWare.EF.TagFilters
{
    public class FugleturDistanceFilter : FugleturTagFilter
    {
        public override IQueryable<Fugletur> Filter(List<Tag> tagList, IQueryable<Fugletur> queryable)
        {
            var distanceTag = tagList
                                .Where(t => t.TagType == TagTypes.Distance)
                                .OrderBy(t => t.SomeValue)
                                .FirstOrDefault() ?? new Tag { SomeValue = 0 };

            var vsopPOI = GeographyPoint.GetPointFromLatLong(GeographyPoint.VSOPLatitude, GeographyPoint.VSOPLongitude);

            return queryable.Where(f => f.Lokalitet.Point != null &&
                   f.Lokalitet.Point.Distance(vsopPOI) <= distanceTag.SomeValue * GeographyPoint.metersPerKilometer);
        }
    }
}
