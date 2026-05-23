using BirdWare.Domain.Entities;
using BirdWare.Domain.Models;

namespace BirdWare.EF.TagFilters
{
    public class ObservationMaanedFilter(BirdWareContext birdWareContext) : ObservationTagFilter
    {
        public override IQueryable<Observation> Filter(List<Tag> tagList, IQueryable<Observation> queryable)
        {
            var tagIdList = GetTagIds(tagList);
            var withMaaned = birdWareContext.Fugletur.GetAarMaaned().Where(y => tagIdList.Contains(y.Maaned));
            return queryable.Where(o => withMaaned.Any(a => a.FugleturId == o.FugleturId));
        }
    }
}
