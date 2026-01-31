using BirdWare.Domain.Entities;
using BirdWare.Domain.Models;

namespace BirdWare.EF.TagFilters
{
    public class FugleturMaanedFilter(BirdWareContext birdWareContext) : FugleturTagFilter
    {
        public override IQueryable<Fugletur> Filter(List<Tag> tagList, IQueryable<Fugletur> queryable)
        {
            var tagIdsByTypeList = GetTagIdsByType(tagList, TagTypes.Maaned);
            var withMaaned = birdWareContext.Fugletur.GetAarMaaned().Where(y => tagIdsByTypeList.Contains(y.Maaned));
            return queryable.Where(o => withMaaned.Any(a => a.FugleturId == o.Id));
        }
    }
}