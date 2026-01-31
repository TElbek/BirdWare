using BirdWare.Domain.Entities;
using BirdWare.Domain.Models;

namespace BirdWare.EF.TagFilters
{
    public class FugleturRegionFilter : FugleturTagFilter
    {
        public override IQueryable<Fugletur> Filter(List<Tag> tagList, IQueryable<Fugletur> queryable)
        {
            var tagIdsByTypeList = GetTagIdsByType(tagList, TagTypes.Region);
            return queryable.Where(g => tagIdsByTypeList.Contains(g.Lokalitet.RegionId));
        }
    }
}
