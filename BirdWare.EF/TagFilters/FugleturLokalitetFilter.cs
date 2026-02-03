using BirdWare.Domain.Entities;
using BirdWare.Domain.Models;

namespace BirdWare.EF.TagFilters
{
    public class FugleturLokalitetFilter : FugleturTagFilter
    {
        public override IQueryable<Fugletur> Filter(List<Tag> tagList, IQueryable<Fugletur> queryable)
        {
            var tagIdsByTypeList = GetTagIds(tagList);
            return queryable.Where(g => tagIdsByTypeList.Contains(g.LokalitetId));
        }
    }
}
