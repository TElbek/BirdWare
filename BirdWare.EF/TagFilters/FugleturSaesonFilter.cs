using BirdWare.Domain.Entities;
using BirdWare.Domain.Models;

namespace BirdWare.EF.TagFilters
{
    public class FugleturSaesonFilter(BirdWareContext birdWareContext) : FugleturTagFilter
    {
        public override IQueryable<Fugletur> Filter(List<Tag> tagList, IQueryable<Fugletur> queryable)
        {
            var seasonTagTypes = tagList.Where(q => ErSaesonTagType(q)).Select(s => s.TagType).ToList();
            var months = SaesonMaaneder.Liste.Where(q => seasonTagTypes.Contains(q.Key)).SelectMany(s => s.Value).ToList();
            var withMaaned = birdWareContext.Fugletur.GetAarMaaned().Where(y => months.Contains(y.Maaned));
            return queryable.Where(o => withMaaned.Any(a => a.FugleturId == o.Id));
        }
    }
}
