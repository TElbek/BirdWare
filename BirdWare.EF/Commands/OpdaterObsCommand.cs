using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;

namespace BirdWare.EF.Commands
{
    public class OpdaterObsCommand(BirdWareContext birdWareContext) : ContextBase(birdWareContext), IOpdaterObsCommand
    {
        public bool OpdaterObservation(VObs vObs)
        {
            if (birdWareContext.Observation.Any(q => q.Id == vObs.ObservationId))
            {
                var observation = birdWareContext.Observation.First(q => q.Id == vObs.ObservationId);
                observation.Beskrivelse = vObs.Bem;
                birdWareContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
