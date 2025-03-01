using BirdWare.Domain.Models;

namespace BirdWare.EF.Interfaces
{
    public interface ISynchTripCommand
    {
        bool PostTrip(SynchTrip synchTrip);
    }
}
