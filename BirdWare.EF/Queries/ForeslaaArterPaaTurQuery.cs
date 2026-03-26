using BirdWare.Domain.Entities;
using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BirdWare.EF.Queries
{
    public class ForeslaaArterPaaTurQuery(BirdWareContext birdWareContext) : IForeslaaArterPaaTurQuery
    {
        public List<ArtForslag> ForeslaaArterSenesteTur()
        {
            var fugleTurId = GetSenesteFugleTurId();

            var seteArterSenesteFugletur = GetSpeciesOnTripAlready(fugleTurId);
            long maaned = GetMaanedForSenesteFugletur(fugleTurId);

            var otherTrips = GetAndreFugletureMedMindstEnArt(seteArterSenesteFugletur);

            var maanedStart = GetMaanedStart(maaned);
            var maanedSlut = GetMaanedSlut(maaned);
            var withMaaned = GetWithMaaned(maanedStart, maanedSlut);

            var artList = (from o in birdWareContext.Observation.AsNoTracking()
                           join a in birdWareContext.Art.AsNoTracking() on o.ArtId equals a.Id
                           join g in birdWareContext.Gruppe.AsNoTracking() on a.GruppeId equals g.Id
                           join fa in birdWareContext.Familie.AsNoTracking() on g.FamilieId equals fa.Id
                           join f in birdWareContext.Fugletur.AsNoTracking() on o.FugleturId equals f.Id
                           join l in birdWareContext.Lokalitet.AsNoTracking() on f.LokalitetId equals l.Id
                           where otherTrips.Contains(f.Id) &&
                                !seteArterSenesteFugletur.Contains(a.Id) &&
                                 withMaaned.Any(s => s.FugleturId == f.Id) &&
                                 l.RegionId > 0
                           group o by new { a.Id, Artnavn = a.Navn, FamilieNavn = fa.Navn } into oGroup
                           orderby oGroup.Count() descending
                           select new ArtForslag
                           {
                               ArtId = oGroup.Key.Id,
                               ArtNavn = oGroup.Key.Artnavn,
                               FamilieNavn = oGroup.Key.FamilieNavn
                           })
                          .Take(40);

            return [.. artList];
        }

        private IQueryable<FugleturAarMaaned> GetWithMaaned(long maanedStart, long maanedSlut) => 
                    birdWareContext.Fugletur
                        .GetAarMaaned()
                        .Where(a => a.Maaned >= maanedStart && a.Maaned <= maanedSlut);

        private static long GetMaanedSlut(long maaned) => Math.Min(12, maaned + 1);

        private static long GetMaanedStart(long maaned) => Math.Max(1, maaned - 1);

        private IQueryable<long> GetAndreFugletureMedMindstEnArt(IQueryable<long> seteArterSenesteFugletur) => 
                    birdWareContext.Observation
                            .Where(q => seteArterSenesteFugletur.Contains(q.ArtId))
                            .Select(s => s.FugleturId).Distinct();

        private long GetMaanedForSenesteFugletur(long fugleTurId) => 
                    birdWareContext.Fugletur.First(q => q.Id == fugleTurId).Maaned;

        private IQueryable<long> GetSpeciesOnTripAlready(long fugleTurId) => 
                    birdWareContext.Observation
                            .Where(q => q.FugleturId == fugleTurId)
                            .Select(s => s.ArtId);

        private long GetSenesteFugleTurId() => 
                    birdWareContext.Fugletur.Max(m => m.Id);
    }
}