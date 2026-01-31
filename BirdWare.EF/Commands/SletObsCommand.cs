using BirdWare.EF.Interfaces;

namespace BirdWare.EF.Commands
{
    public class SletObsCommand(BirdWareContext birdWareContext) : ISletObsCommand
    {
        public void SletObservation(long observationId)
        {
            var fugleturId = birdWareContext.Fugletur.Max(m => m.Id);

            var obs = birdWareContext.Observation.SingleOrDefault(o => o.Id == observationId && o.FugleturId == fugleturId);
            if (obs != null)
            {
                birdWareContext.Observation.Remove(obs);
                birdWareContext.SaveChanges();
            }
        }
    }
}
