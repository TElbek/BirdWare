using BirdWare.Domain.Entities;
using BirdWare.EF.Interfaces;

namespace BirdWare.EF.Commands
{
    public class OpretObsCommand(BirdWareContext birdWareContext) : ContextBase(birdWareContext), IOpretObsCommand
    {
        public bool OpretObsPåFugletur(long artId)
        {
            var existArt = birdWareContext.Art.Any(q => q.Id == artId);
            if (existArt)
            {
                var latestFugleturId = birdWareContext.Fugletur.Max(a => a.Id);
                var fugletur = birdWareContext.Fugletur.Single(q => q.Id == latestFugleturId);
                var art = birdWareContext.Art.Single(q => q.Id == artId);

                var observation = new Observation {ArtId = artId, 
                                                   Art = art,
                                                   FugleturId = latestFugleturId, 
                                                   Fugletur = fugletur,
                                                   Beskrivelse = "Ej angivet"};

                try 
                {
                    birdWareContext.Observation.Add(observation);
                    birdWareContext.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }
    }
}
