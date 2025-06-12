using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;

namespace BirdWare.EF.Queries
{
    public class ArterAarQueries(BirdWareContext birdWareContext) : ContextBase(birdWareContext), IArterAarQueries
    {
        public IQueryable<ArterAar> GetArterIAar()
        {
            return GetArterAar(DateTime.Now.Year);
        }

        public IQueryable<ArterAar> GetArterSidsteAar()
        {
            var artersidsteAar = GetArterAar(DateTime.Now.Year - 1);

            var artersidsteAarSammePeriode = artersidsteAar
                  .Where(q => q.Dato.HasValue && q.Dato.Value <= DateTime.Now.AddYears(-1));

            return artersidsteAarSammePeriode;
        }

        private IQueryable<ArterAar> GetArterAar(long aarstal)
        {
            var arterAar = from o in birdWareContext.Observation join 
                        a in birdWareContext.Art on o.ArtId equals a.Id join
                        f in birdWareContext.Fugletur on o.FugleturId equals f.Id join 
                        l in birdWareContext.Lokalitet on f.LokalitetId equals l.Id
                  where f.Dato.HasValue && f.Dato.Value.Year == aarstal && l.RegionId > 0
                  select new { o.ArtId, a.GruppeId, a.Navn, a.SU, a.Speciel, f.Dato, FugleturId = f.Id };

                 return from o in arterAar
                  group o by new { o.ArtId, o.GruppeId, o.Navn, o.SU, o.Speciel } into ogroup
                  select new ArterAar
                  {
                      ArtNavn = ogroup.Key.Navn,
                      ArtId = ogroup.Key.ArtId,
                      GruppeId = ogroup.Key.GruppeId,
                      SU = ogroup.Key.SU,
                      Speciel = ogroup.Key.Speciel,
                      FugleturId = ogroup.Min(item => item.FugleturId),
                      Dato = ogroup.Min(item => item.Dato)
                  };
        }
    }
}