using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BirdWare.EF.Queries
{
    internal class AnkomtsDagQuery(BirdWareContext birdWareContext) : ContextBase(birdWareContext), IAnkomtsDagQuery
    {
        public ILookup<long, AnkomstDagBasis> GetAnkomstData(long familieId)
        {
            var rows = birdWareContext.Observation
                .AsNoTracking()
                .Where(o =>
                    o.Art.Gruppe.Familie.Id == familieId &&
                    o.Fugletur.Dato != null &&
                    o.Art.SetIDK &&
                   !o.Art.SU &&
                    o.Fugletur.Lokalitet.RegionId > 0)
                .GroupBy(o => new { o.Art.Id, o.Art.Navn, Aarstal = o.Fugletur.Dato!.Value.Year })
                .Select(g => new
                {
                    g.Key.Id,
                    g.Key.Navn,
                    g.Key.Aarstal,
                    MinDato = g.Min(x => x.Fugletur.Dato)
                }).AsEnumerable();

            var result = rows
                .Select(r => new AnkomstDagBasis
                {
                    ArtId = r.Id,
                    ArtNavn = r.Navn ?? string.Empty,
                    Aarstal = r.Aarstal,
                    AnkomstDag = r.MinDato.HasValue ? r.MinDato.Value.DayOfYear : 0
                })
                .ToLookup(x => x.ArtId);

            return result;
        }
    }
}