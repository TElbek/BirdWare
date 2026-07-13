using BirdWare.Domain.Models;

namespace BirdWare.EF.Interfaces
{
    public interface IFugleturObservationQuery
    {
        IEnumerable<VObs> GetObservationer(long FugleturId);
    }
}
