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
                if (!birdWareContext.Fugletur.Any(r => r.Id == synchTrip.Fugletur.FugleturId) &&
                    birdWareContext.Lokalitet.Any(q => q.Id == synchTrip.Fugletur.LokalitetId))
                {
                    var fugletur = new Fugletur()
                    {
                        Id = synchTrip.Fugletur.FugleturId,
                        Dato = synchTrip.Fugletur.Dato,
                        LokalitetId = synchTrip.Fugletur.LokalitetId,
                        Lokalitet = birdWareContext.Lokalitet.First(q => q.Id == synchTrip.Fugletur.LokalitetId)
                    };

                    birdWareContext.Fugletur.Add(fugletur);
                }

                birdWareContext.Observation.RemoveRange(
                    birdWareContext.Observation.Where(t => t.FugleturId == synchTrip.Fugletur.FugleturId));

                birdWareContext.SaveChanges();

                foreach (var obs in synchTrip.Fugletur.Observation)
                {
                    if (birdWareContext.Art.Any(q => q.Id == obs.ArtId))
                    {
                        var observation = new Observation()
                        {
                            ArtId = obs.ArtId,
                            Beskrivelse = obs.Beskrivelse,
                            FugleturId = synchTrip.Fugletur.FugleturId,
                            Art = birdWareContext.Art.First(q => q.Id == obs.ArtId),
                            Fugletur = birdWareContext.Fugletur.First(q => q.Id == synchTrip.Fugletur.FugleturId)
                        };

                        birdWareContext.Observation.Add(observation);
                    }
                }

                birdWareContext.SaveChanges();
                transaction.Commit();
                return true;

            }
            catch (Exception ex)
			{
                return false;
			}
        }
    }
}