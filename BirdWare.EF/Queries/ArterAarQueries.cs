using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;

namespace BirdWare.EF.Queries
{
    internal class ArterAarQueries(BirdWareContext birdWareContext) : ContextBase(birdWareContext), IArterAarQueries
    {
        public IQueryable<ArterAar> GetArterIAar()
        {
            return GetArterAar(DateTime.Now.Year);
        }

        public IQueryable<ArterAar> GetArterSidsteAar()
        {
            return GetArterAar(DateTime.Now.Year - 1)
                  .Where(q => q.Dato <= DateTime.Now.AddYears(-1));
        }

        private IQueryable<ArterAar> GetArterAar(long aarstal)
        {
            return
                  from o in birdWareContext.Observation
                  join a in birdWareContext.Art on o.ArtId equals a.Id
                  join f in birdWareContext.Fugletur on o.FugleturId equals f.Id
                  join l in birdWareContext.Lokalitet on f.LokalitetId equals l.Id
                  where f.Dato.HasValue && f.Dato.Value.Year == aarstal && l.RegionId > 0
                  group o by new { o.ArtId, a.GruppeId, a.Navn, a.SU } into ogroup
                  select new ArterAar
                  {
                      ArtNavn = ogroup.Key.Navn,
                      ArtId = ogroup.Key.ArtId,
                      GruppeId = ogroup.Key.GruppeId,
                      SU = ogroup.Key.SU,
                      FugleturId = ogroup.Min(item => item.FugleturId),
                      Dato = ogroup.Min(item2 => item2.Fugletur.Dato)
                  };
        }
    }
}