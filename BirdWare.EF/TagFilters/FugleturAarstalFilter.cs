using BirdWare.Domain.Entities;
using BirdWare.Domain.Models;

namespace BirdWare.EF.TagFilters
{
    public class FugleturAarstalFilter(BirdWareContext birdWareContext) : FugleturTagFilter
    {
        public override IQueryable<Fugletur> Filter(List<Tag> tagList, IQueryable<Fugletur> queryable)
        {
            var tagIdList = GetTagIds(tagList);
            var withAarstal = birdWareContext.Fugletur.GetAarMaaned().Where(y => tagIdList.Contains(y.Aarstal));
            return queryable.Where(o => withAarstal.Any(a => a.FugleturId == o.Id));
        }
    }
}
