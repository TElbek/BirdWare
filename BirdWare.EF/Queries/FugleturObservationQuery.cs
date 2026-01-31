using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BirdWare.EF.Queries
{
    public class FugleturObservationQuery(BirdWareContext birdWareContext) : IFugleturObservationQuery
    {
        public List<VObs> GetObservationer(long FugleturId)
        {
            var list = from o in birdWareContext.Observation.AsNoTracking() join
                            a in birdWareContext.Art.AsNoTracking() on o.ArtId equals a.Id join
                            g in birdWareContext.Gruppe.AsNoTracking() on a.GruppeId equals g.Id join
                            f in birdWareContext.Familie.AsNoTracking() on g.FamilieId equals f.Id
                       where o.FugleturId == FugleturId
                       orderby f.Navn, a.Navn
                       select new VObs
                       {
                           ObservationId = o.Id,
                           ArtId = a.Id,
                           ArtNavn = a.Navn ?? string.Empty,
                           Bem = o.Beskrivelse ?? string.Empty,
                           FamilieId = f.Id,
                           FamilieNavn = f.Navn ?? string.Empty,
                           FugleturId = o.FugleturId,
                           GruppeId = g.Id,
                           SU = a.SU,
                           Speciel = a.Speciel
                       };
                            
            return [.. list];
        }        
    }
}