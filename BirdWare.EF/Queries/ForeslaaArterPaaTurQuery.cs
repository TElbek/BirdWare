using BirdWare.Domain.Entities;
using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;

namespace BirdWare.EF.Queries
{
    public class ForeslaaArterPaaTurQuery(BirdWareContext birdWareContext) : IForeslaaArterPaaTurQuery
    {
        public List<ArtForslag> ForeslaaArterSenesteTur()
        {
            var fugleTurId = birdWareContext.Fugletur.Max(m => m.Id);

            var speciesOnTripAlready = birdWareContext.Observation
                .Where(q => q.FugleturId == fugleTurId)
                .Select(s => s.ArtId);

            var maaned = birdWareContext.Fugletur.First(q => q.Id == fugleTurId).Maaned;

            var otherTrips = birdWareContext.Observation
                .Where(q => speciesOnTripAlready.Contains(q.ArtId))
                .Select(s => s.FugleturId).Distinct();

            var maanedStart = Math.Max(1, maaned - 1);
            var maanedSlut  = Math.Min(12, maaned + 1);
            var withMaaned = birdWareContext.Fugletur.GetAarMaaned()
                                .Where(a => a.Maaned >= maanedStart && a.Maaned <= maanedSlut);

            var artList = (from o in birdWareContext.Observation
                           join a in birdWareContext.Art on o.ArtId equals a.Id
                           join g in birdWareContext.Gruppe on a.GruppeId equals g.Id
                           join f in birdWareContext.Fugletur on o.FugleturId equals f.Id
                           join l in birdWareContext.Lokalitet on f.LokalitetId equals l.Id
                           where otherTrips.Contains(f.Id) &&
                                !speciesOnTripAlready.Contains(a.Id) &&
                                 withMaaned.Any(s => s.FugleturId == f.Id) &&
                                 l.RegionId > 0
                           group o by new { a.Id, Artnavn = a.Navn, GruppeNavn = g.Navn } into oGroup
                           orderby oGroup.Count() descending
                           select new ArtForslag { ArtId = oGroup.Key.Id, 
                                                   ArtNavn = oGroup.Key.Artnavn, 
                                                   GruppeNavn = oGroup.Key.GruppeNavn, 
                                                   Indeks = (oGroup.Count() - oGroup.Count() % 10) })
                          .Take(20);

            return [.. artList];
        }
    }
}
