using BirdWare.Domain.Entities;
using BirdWare.Domain.Models;

namespace BirdWare.EF.TagFilters
{
    public class ObservationSenesteAarFilter : ObservationTagFilter
    {
        public override IQueryable<Observation> Filter(List<Tag> tagList, IQueryable<Observation> queryable)
        {
            var tagIdsByTypeList = GetTagIds(tagList);
            return queryable.Where(q => q.Fugletur.Dato >= DateTime.Now.AddYears(-1 * (int)tagIdsByTypeList.First()));
        }
    }
}
