using BirdWare.Domain.Models;

namespace BirdWare.EF.Interfaces
{
    public interface IOpdaterObsCommand
    {
        bool OpdaterObservation(VObs vObs);
    }
}
