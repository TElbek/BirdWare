using BirdWare.Domain.Entities;
using BirdWare.Domain.Models;

namespace BirdWare.EF.TagFilters
{
    public class FugleturLandFilter : FugleturTagFilter
    {
        public override IQueryable<Fugletur> Filter(List<Tag> tagList, IQueryable<Fugletur> queryable)
        {
            return tagList.Any(q => q.Name == "Danmark") ? 
                queryable.Where(g => g.Lokalitet.RegionId > 0) : 
                queryable;
        }       
    }           
}
