using BirdWare.Domain.Entities;
using BirdWare.Domain.Models;

namespace BirdWare.EF.TagFilters
{
    public class ObservationRegionFilter : ObservationTagFilter
    {
        public override IQueryable<Observation> Filter(List<Tag> tagList, IQueryable<Observation> queryable)
        {
            var tagIdList = GetTagIds(tagList);
            return queryable.Where(g => tagIdList.Contains(g.Fugletur.Lokalitet.RegionId));
        }
    }
}
