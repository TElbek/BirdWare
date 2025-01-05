using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;

namespace BirdWare.EF.Queries
{
    public class FugleturQuery(BirdWareContext birdWareContext) : IFugleturQuery
    {
        public VTur GetFugletur(long id)
        {
            var vtur = from f in birdWareContext.Fugletur
                       join
                            l in birdWareContext.Lokalitet on f.LokalitetId equals l.Id
                       join
                            r in birdWareContext.Region on l.RegionId equals r.Id
                       where f.Id == id
                       select new VTur
                       {
                           Aarstal = f.Aarstal,
                           Maaned = f.Maaned,
                           Dato = f.Dato,
                           Id = f.Id,
                           LokalitetId = l.Id,
                           LokalitetNavn = l.Navn ?? string.Empty,
                           RegionId = r.Id,
                           RegionNavn = r.Navn ?? string.Empty
                       };

            return vtur.First();
        }

        public long GetSenesteFugletur()
        {
            return birdWareContext.Fugletur.Max(f => f.Id);
        }
    }
}
