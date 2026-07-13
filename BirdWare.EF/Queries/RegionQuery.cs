using BirdWare.Domain.Entities;
using BirdWare.EF.Interfaces;

namespace BirdWare.EF.Queries
{
    public class RegionQuery(BirdWareContext birdWareContext) : ContextBase(birdWareContext), IRegionQuery
    {
        public IEnumerable<Region> GetList(bool inklUdland = false)
        {
            return birdWareContext.Region
                        .Where(q => inklUdland || q.Id > 0)
                        .OrderBy(o => o.Navn);
        }
    }
}
