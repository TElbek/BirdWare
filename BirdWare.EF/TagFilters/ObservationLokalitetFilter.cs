using BirdWare.Domain.Entities;
using BirdWare.Domain.Models;

namespace BirdWare.EF.TagFilters
{
    public class ObservationLokalitetFilter : ObservationTagFilter
    {
        public override IQueryable<Observation> Filter(List<Tag> tagList, IQueryable<Observation> queryable)
        {
            var tagIdsByTypeList = GetTagIdsByType(tagList, TagTypes.Lokalitet); ;
            return queryable.Where(g => tagIdsByTypeList.Contains(g.Fugletur.LokalitetId));
        }
    }
}
