using BirdWare.Domain.Models;

namespace BirdWare.EF.Interfaces
{
    public interface ISynchTripQuery
    {
        SynchTrip GetTrip(long fugleturId);
    }
}
