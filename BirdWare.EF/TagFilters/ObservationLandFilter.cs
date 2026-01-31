using BirdWare.Domain.Entities;
using BirdWare.Domain.Models;

namespace BirdWare.EF.TagFilters
{
    public class ObservationLandFilter : ObservationTagFilter
    {
        public override IQueryable<Observation> Filter(List<Tag> tagList, IQueryable<Observation> queryable)
        {
            return tagList.Any(q => q.Name == "Danmark") ? 
                queryable.Where(g => g.Fugletur.Lokalitet.RegionId > 0) : 
                queryable;
        }       
    }           
}
