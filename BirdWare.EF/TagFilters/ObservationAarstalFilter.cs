using BirdWare.Domain.Entities;
using BirdWare.Domain.Models;

namespace BirdWare.EF.TagFilters
{
    public class ObservationAarstalFilter(BirdWareContext birdWareContext) : ObservationTagFilter
    {
        public override IQueryable<Observation> Filter(List<Tag> tagList, IQueryable<Observation> queryable)
        {
            var tagIdsByTypeList = GetTagIdsByType(tagList, TagTypes.Aarstal);
            var withAarstal = birdWareContext.Fugletur.GetAarMaaned().Where(y => tagIdsByTypeList.Contains(y.Aarstal));
            return queryable.Where(o => withAarstal.Any(a => a.FugleturId == o.FugleturId));
        }
    }
}
