using BirdWare.Domain.Models;
using BirdWare.Domain.Utilities;
using BirdWare.EF.Geography;
using BirdWare.EF.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BirdWare.EF.Queries
{
    public class HvorKanJegFindeQuery(BirdWareContext birdWareContext) : IHvorKanJegFindeQuery
    {
        public List<ObsTidligereAar> GetObsTidligereAar()
        {
            var startDag = DateCalculations.GetRelativDagIAaret(DateCalculations.TrediveDageTidligere);
            var slutdag = DateCalculations.GetRelativDagIAaret(DateCalculations.OmTrediveDage);

            return
                [.. from o in birdWareContext.Observation.AsNoTracking()
                 join f in birdWareContext.Fugletur.AsNoTracking() on o.FugleturId equals f.Id
                 join l in birdWareContext.Lokalitet.AsNoTracking() on f.LokalitetId equals l.Id
                 where GetArterJegIkkeHarSetIAar().Contains(o.ArtId) &&
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
                     Distance = l.Point!.Distance(GetVSOPPOI()) / GeographyPoint.metersPerKilometer
                 }];
        }

        private IQueryable<long> GetArterJegIkkeHarSetIAar()
        {
            return birdWareContext.Art.AsNoTracking()
                    .Where(a => !birdWareContext.Observation.AsNoTracking()
                    .Any(o => o.ArtId == a.Id && o.Fugletur.Dato >= GetNytaarsDagIAar()))
                    .Where(q => q.SU == false && q.Speciel == false)
                    .Select(a => a.Id);
        }

        private static DateTime GetNytaarsDagIAar()
        {
            return new DateTime(DateTime.Now.Year, 1, 1);
        }

        private static NetTopologySuite.Geometries.Point GetVSOPPOI()
        {
            return GeographyPoint.GetPointFromLatLong(
                        GeographyPoint.VSOPLatitude,
                        GeographyPoint.VSOPLongitude);
        }
    }
}