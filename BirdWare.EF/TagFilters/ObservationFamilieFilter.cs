using BirdWare.Domain.Entities;
using BirdWare.Domain.Models;

namespace BirdWare.EF.TagFilters
{
    public class ObservationFamilieFilter : ObservationTagFilter
    {
        public override IQueryable<Observation> Filter(List<Tag> tagList, IQueryable<Observation> queryable)
        {
            var tagIdsByTypeList = GetTagIds(tagList);
            return queryable.Where(g => tagIdsByTypeList.Contains(g.Art.Gruppe.FamilieId));
        }
    }
}
