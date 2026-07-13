using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;

namespace BirdWare.EF.Queries
{
    internal class AnkomtsDagQuery(BirdWareContext birdWareContext) : ContextBase(birdWareContext), IAnkomtsDagQuery
    {
        public ILookup<long, AnkomstDagBasis> GetAnkomstData(long familieId)
        {
            return (from obs in birdWareContext.Observation
                    join art in birdWareContext.Art on obs.ArtId equals art.Id
                    join grupper in birdWareContext.Gruppe on art.GruppeId equals grupper.Id
                    join familier in birdWareContext.Familie on grupper.FamilieId equals familier.Id
                    join fugletur in birdWareContext.Fugletur on obs.FugleturId equals fugletur.Id
                    join lokalitet in birdWareContext.Lokalitet on fugletur.LokalitetId equals lokalitet.Id
                    join region in birdWareContext.Region on lokalitet.RegionId equals region.Id
                    where familier.Id == familieId &&
                                  art.SetIDK == true &&
                                  art.SU == false &&
                                  region.Id > 0
                    group obs by new
                    {
                        art.Id,
                        art.Navn,
                        Aarstal = fugletur.Dato.HasValue ? fugletur.Dato.Value.Year : DateTime.MinValue.Year
                    }
                   into g
                    select new AnkomstDagBasis
                    {
                        ArtId = g.Key.Id,
                        ArtNavn = g.Key.Navn,
                        Aarstal = g.Key.Aarstal,
                        AnkomstDag = g.Where(o => o.Fugletur.Dato.HasValue)
                                      .Min(o => o.Fugletur.Dato.Value.DayOfYear),
                    }).ToLookup(x => x.ArtId);
        }
    }
}