using BirdWare.Domain.Entities;
using BirdWare.Domain.Models;

namespace BirdWare.EF.TagFilters
{
    public class FugleturLokalitetFilter : FugleturTagFilter
    {
        public override IQueryable<Fugletur> Filter(List<Tag> tagList, IQueryable<Fugletur> queryable)
        {
            var tagIdsByTypeList = GetTagIdsByType(tagList, TagTypes.Lokalitet); ;
            return queryable.Where(g => tagIdsByTypeList.Contains(g.LokalitetId));
        }
    }
}
