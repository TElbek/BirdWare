using BirdWare.Domain.Entities;
using BirdWare.Domain.Models;

namespace BirdWare.EF.TagFilters
{
    public class ObservationGruppeFilter : ObservationTagFilter
    {
        public override IQueryable<Observation> Filter(List<Tag> tagList, IQueryable<Observation> queryable)
        {
            var tagIdsByTypeList = GetTagIdsByType(tagList, TagTypes.Gruppe); ;
            return queryable.Where(g => tagIdsByTypeList.Contains(g.Art.GruppeId));
        }
    }
}
