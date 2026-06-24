using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;

namespace BirdWare.EF.Queries
{
    public class BedsteMaanedForFamilieQuery(BirdWareContext birdWareContext) : ContextBase(birdWareContext), IBedsteMaanedForFamilieQuery
    {
        public List<BedsteMaanedForFamilie> GetBedsteMaanedForFamilie()
        {
            var perMonth = birdWareContext.Observation
                .Where(o => o.Fugletur != null && 
                            o.Fugletur.Lokalitet != null && 
                            o.Fugletur.Lokalitet.RegionId > 0 &&
                            o.Fugletur.Dato.HasValue &&
                            o.Fugletur.Dato.Value > DateTime.Now.AddYears(-10) &&
                            o.Art != null)
                .GroupBy(o => new
                {
                    Maaned = o.Fugletur.Dato.HasValue ? o.Fugletur.Dato.Value.Month : 0,
                    FamilieNavn = o.Art.Gruppe.Familie.Navn
                })
                .Select(g => new
                {
                    g.Key.Maaned,
                    g.Key.FamilieNavn,
                    AntalArter = g.Select(x => x.ArtId).Distinct().Count()
                }).AsEnumerable();

            var bestPerFamily = perMonth
                .GroupBy(x => x.FamilieNavn)
                .Select(g => g.OrderByDescending(x => x.AntalArter).ThenBy(k => k.Maaned).First())
                .Select(x => new BedsteMaanedForFamilie(x.Maaned, x.FamilieNavn ?? string.Empty, x.AntalArter))
                .ToList();

            return [.. bestPerFamily];
        }
    }
}
