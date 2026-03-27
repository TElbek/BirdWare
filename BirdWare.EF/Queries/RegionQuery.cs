using BirdWare.Domain.Entities;
using BirdWare.EF.Interfaces;

namespace BirdWare.EF.Queries
{
    public class RegionQuery(BirdWareContext birdWareContext) : ContextBase(birdWareContext), IRegionQuery
    {
        public List<Region> GetList()
        {
            return [.. birdWareContext.Region
                        .Where(q => q.Id >= 0)
                        .OrderBy(o => o.Navn)];
        }
    }
}
