using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BirdWare.EF.Queries
{
    internal class AnkomtsDagSubQuery(IDbContextFactory<BirdWareContext> dbContextFactory) : IAnkomtsDagSubQuery
    {
        private readonly IDbContextFactory<BirdWareContext> dbContextFactory = dbContextFactory;

        public async Task<List<AnkomstDagBeregning>> GetAnkomstDagQuery(long familieId)
        {
            var birdWareContext = await dbContextFactory.CreateDbContextAsync();

            return await (from obs in birdWareContext.Observation
                          join art in birdWareContext.Art on obs.ArtId equals art.Id
                          join grupper in birdWareContext.Gruppe on art.GruppeId equals grupper.Id
                          join fugletur in birdWareContext.Fugletur on obs.FugleturId equals fugletur.Id
                          join lokalitet in birdWareContext.Lokalitet on fugletur.LokalitetId equals lokalitet.Id
                          join region in birdWareContext.Region on lokalitet.RegionId equals region.Id
                          where grupper.FamilieId == familieId &&
                                        art.SetIDK == true &&
                                        art.SU == false &&
                                        fugletur.Dato.HasValue &&
                                        region.Id > 0
                          group obs by new { art.Id, art.Navn, art.Speciel, art.SU, Aarstal = fugletur.Dato.HasValue ? fugletur.Dato.Value.Year : DateTime.MinValue.Year } into g
                          select new AnkomstDagBeregning
                          {
                              ArtId = g.Key.Id,
                              ArtNavn = g.Key.Navn,
                              Speciel = g.Key.Speciel,
                              SU = g.Key.SU,
                              Aarstal = g.Key.Aarstal,
                              AnkomstDag = g.Min(o => o.Fugletur.Dato.HasValue ? o.Fugletur.Dato.Value.DayOfYear : 0)
                          }).ToListAsync();
        }

    }
}