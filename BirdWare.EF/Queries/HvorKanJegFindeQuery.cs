using BirdWare.Domain.Models;
using BirdWare.EF.Geography;
using BirdWare.EF.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BirdWare.EF.Queries
{
    public class HvorKanJegFindeQuery(BirdWareContext birdWareContext) : IHvorKanJegFindeQuery
    {

        public List<ObsTidligereAar> GetObsTidligereAar(int startDag, int slutdag, NetTopologySuite.Geometries.Point vsopPOI, IQueryable<long> arterJegIkkeHarSetIAar)
        {
            return
                [.. from o in birdWareContext.Observation.AsNoTracking()
                 join f in birdWareContext.Fugletur.AsNoTracking() on o.FugleturId equals f.Id
                 join l in birdWareContext.Lokalitet.AsNoTracking() on f.LokalitetId equals l.Id
                 where arterJegIkkeHarSetIAar.Contains(o.ArtId) &&
                     o.Fugletur.Dato.HasValue &&
                     o.Fugletur.Dato.Value.DayOfYear >= startDag &&
                     o.Fugletur.Dato.Value.DayOfYear <= slutdag &&
                     o.Fugletur.Lokalitet.RegionId > 0 &&
                     l.Point != null &
                     l.RegionId > 0
                 select new ObsTidligereAar
                 {
                     ArtId = o.Art.Id,
                     ArtNavn = o.Art.Navn ?? string.Empty,
                     GruppeNavn = o.Art.Gruppe.Navn ?? string.Empty,
                     FamilieNavn = o.Art.Gruppe.Familie.Navn ?? string.Empty,
                     LokalitetId = l.Id,
                     LokalitetNavn = l.Navn ?? string.Empty,
                     Distance = l.Point!.Distance(vsopPOI) / GeographyPoint.metersPerKilometer
                 }];
        }

        public IQueryable<long> GetArterJegIkkeHarSetIAar(DateTime nytaarsDagIAar)
        {
            return birdWareContext.Art.AsNoTracking()
                    .Where(a => !birdWareContext.Observation.AsNoTracking()
                    .Any(o => o.ArtId == a.Id && o.Fugletur.Dato >= nytaarsDagIAar))
                    .Where(q => q.SU == false && q.Speciel == false)
                    .Select(a => a.Id);
        }

        public NetTopologySuite.Geometries.Point GetVSOPPOI()
        {
            return GeographyPoint.GetPointFromLatLong(
                        GeographyPoint.VSOPLatitude,
                        GeographyPoint.VSOPLongitude);
        }
    }
}
