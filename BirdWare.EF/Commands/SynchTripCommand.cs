using BirdWare.Domain.Entities;
using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;

namespace BirdWare.EF.Commands
{
    public class SynchTripCommand(BirdWareContext birdWareContext) : ContextBase(birdWareContext), ISynchTripCommand
    {
        public bool PostTrip(SynchTrip synchTrip)
        {
			try
			{
                using var transaction = birdWareContext.Database.BeginTransaction();
                if (!birdWareContext.Fugletur.Any(r => r.Id == synchTrip.Fugletur.FugleturId))
                {
                    var fugletur = new Fugletur()
                    {
                        Id = synchTrip.Fugletur.FugleturId,
                        Dato = synchTrip.Fugletur.Dato,
                        LokalitetId = synchTrip.Fugletur.LokalitetId
                    };

                    birdWareContext.Fugletur.Add(fugletur);
                }

                birdWareContext.Observation.RemoveRange(
                    birdWareContext.Observation.Where(t => t.FugleturId == synchTrip.Fugletur.FugleturId));

                foreach (var obs in synchTrip.Fugletur.Observation)
                {
                    var observation = new Observation()
                    {
                        ArtId = obs.ArtId,
                        Beskrivelse = obs.Beskrivelse,
                        FugleturId = synchTrip.Fugletur.FugleturId
                    };

                    birdWareContext.Observation.Add(observation);
                }

                birdWareContext.SaveChanges();
                transaction.Commit();
                return true;

            }
            catch (Exception)
			{
                return false;
			}
        }
    }
}