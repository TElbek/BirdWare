using BirdWare.Domain.Entities;
using BirdWare.Domain.Models;

namespace BirdWare.EF.TagFilters
{
    public class FugleturKommuneFilter : FugleturTagFilter
    {
        public override IQueryable<Fugletur> Filter(List<Tag> tagList, IQueryable<Fugletur> queryable)
        {
            var tagIdList = GetTagIds(tagList);
            return queryable.Where(g => tagIdList.Contains(g.Lokalitet.KommuneId));

        }
    }
}
