using BirdWare.Domain.Models;

namespace BirdWare.EF.Interfaces
{
    public interface IFugleturObservationQuery
    {
        List<VObs> GetObservationer(long FugleturId);
        void SletObservation(long id);
    }
}
