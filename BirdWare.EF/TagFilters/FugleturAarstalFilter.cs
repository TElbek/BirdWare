using BirdWare.Domain.Entities;
using BirdWare.Domain.Models;

namespace BirdWare.EF.TagFilters
{
    public class FugleturAarstalFilter(BirdWareContext birdWareContext) : FugleturTagFilter
    {
        public override IQueryable<Fugletur> Filter(List<Tag> tagList, IQueryable<Fugletur> queryable)
        {
            var tagIdsByTypeList = GetTagIdsByType(tagList, TagTypes.Aarstal);
            var withAarstal = birdWareContext.Fugletur.GetAarMaaned().Where(y => tagIdsByTypeList.Contains(y.Aarstal));
            return queryable.Where(o => withAarstal.Any(a => a.FugleturId == o.Id));
        }
    }
}
