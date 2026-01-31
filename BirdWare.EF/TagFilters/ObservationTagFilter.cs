using BirdWare.Domain.Entities;
using BirdWare.EF.Interfaces;

namespace BirdWare.EF.TagFilters
{
    public abstract class ObservationTagFilter : BaseTagFilter<Observation>, IObservationTagFilter
    {        
    }
}
