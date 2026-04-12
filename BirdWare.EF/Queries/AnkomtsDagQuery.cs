using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;

namespace BirdWare.EF.Queries
{
    public class AnkomtsDagQuery(BirdWareContext birdWareContext) : ContextBase(birdWareContext), IAnkomtsDagQuery
    {
        public List<AnkomstDag> GetAnkomtsDage(long familieId)
        {
            var ankomtsDage = new List<AnkomstDag>();

            var iAarQuery = GetIAarQuery(familieId);

            var ankomstDagQuery = GetAnkomstDagQuery(familieId);

            foreach (var item in ankomstDagQuery.Select(s => new { s.ArtId, s.ArtNavn, s.Speciel, s.SU }).Distinct())
            {
                var ankomstDatoIAar = GetAnkomstDatoIAar(iAarQuery, item.ArtId);

                ankomtsDage.Add(new AnkomstDag
                {
                    ArtId = item.ArtId,
                    ArtNavn = item.ArtNavn,
                    Speciel = item.Speciel,
                    SU = item.SU,
                    AnkomstDato = new DateTime(DateTime.Now.Year, 1, 1).AddDays(GetAnkomstDatoGennemsnit(ankomstDagQuery, item.ArtId)),
                    SetIaarDato = ankomstDatoIAar.HasValue ? new DateTime(DateTime.Now.Year, 1, 1).AddDays(ankomstDatoIAar.Value) : null,
                });
            }

            return [.. ankomtsDage.OrderBy(o => o.ArtNavn)];
        }

        private static double GetAnkomstDatoGennemsnit(List<AnkomstDagBeregning> ankomstDagQuery, long artId)
        {
            var match = ankomstDagQuery.Where(x => x.ArtId == artId);
            return match != null ? match.Average(a => a.AnkomstDag) - 1 : 0;
        }

        private static double? GetAnkomstDatoIAar(List<AnkomstDagBeregning> iAarQuery, long artId)
        {
            var match = iAarQuery.FirstOrDefault(x => x.ArtId == artId);
            return match != null && match.SetIaarDag.HasValue ? match.SetIaarDag.Value - 1 : (double?)null;
        }

        private List<AnkomstDagBeregning> GetAnkomstDagQuery(long familieId)
        {
            return [.. from obs in birdWareContext.Observation
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
                       }];
        }

        private List<AnkomstDagBeregning> GetIAarQuery(long familieId)
        {
            return [.. (from obs in birdWareContext.Observation
                    join art in birdWareContext.Art on obs.ArtId equals art.Id
                    join grupper in birdWareContext.Gruppe on art.GruppeId equals grupper.Id
                    join fugletur in birdWareContext.Fugletur on obs.FugleturId equals fugletur.Id
                    join lokalitet in birdWareContext.Lokalitet on fugletur.LokalitetId equals lokalitet.Id
                    join region in birdWareContext.Region on lokalitet.RegionId equals region.Id
                   where  grupper.FamilieId == familieId && 
                          art.SetIDK == true &&
                          region.Id > 0 &&
                          fugletur.Dato.HasValue && fugletur.Dato.Value.Year == DateTime.Now.Year
                    group obs by new { art.Id, art.Navn } into g
                    select new AnkomstDagBeregning
                    {
                        ArtId = g.Key.Id,
                        ArtNavn = g.Key.Navn,
                        SetIaarDag = g.Min(o => o.Fugletur.Dato.HasValue ? o.Fugletur.Dato.Value.DayOfYear : 0)
                    })];
        }
    }
}