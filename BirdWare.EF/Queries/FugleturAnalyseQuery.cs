using BirdWare.Domain.Entities;
using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BirdWare.EF.Queries
{
    public class FugleturAnalyseQuery(BirdWareContext birdWareContext) : IFugleturAnalyseQuery
    {
        public IQueryable<long> HentArtListe(long fugleturId)
        {
            return birdWareContext.Observation
                .AsNoTracking()
                .Where(o => o.FugleturId == fugleturId)
                .Select(o => o.ArtId);
        }

        public VTur FindFugletur(long fugleturId)
        {
            if (!birdWareContext.Fugletur.Any(q => q.Id == fugleturId)) return new VTur(); 
            
            return birdWareContext.Fugletur.AsNoTracking()
                    .Where(f => f.Id == fugleturId)
                    .Select(f => new VTur
                    {
                        Aarstal = f.Aarstal,
                        Maaned = f.Maaned,
                        Dato = f.Dato,
                        Id = f.Id,
                        LokalitetId = f.LokalitetId,
                        KommuneId = f.Lokalitet.KommuneId,
                        RegionId = f.Lokalitet.RegionId,
                    }).First();
        }

        public IQueryable<TripAnalysisResult> FoersteObsIDatabasen(
            VTur vTur,
            IQueryable<long> arterForTuren)
        {
            return birdWareContext.Art.AsNoTracking()
                .Where(a =>
                    arterForTuren.Contains(a.Id)
                    && !birdWareContext.Observation.AsNoTracking().Any(o =>
                        o.ArtId == a.Id
                        && o.FugleturId < vTur.Id))
                .Select(a => TripAnalysisResultFactory(a, AnalyseTyper.FoersteObsIDatabasen));
        }

        public IQueryable<TripAnalysisResult> FoersteObsIDK(
                    VTur vTur,
                    IQueryable<long> arterForTuren)
        {
            return birdWareContext.Art.AsNoTracking()
                .Where(a =>
                    arterForTuren.Contains(a.Id)
                    && !birdWareContext.Observation.AsNoTracking().Any(o =>
                        o.ArtId == a.Id
                        && o.FugleturId < vTur.Id
                        && o.Fugletur.Lokalitet.RegionId > 0))
                .Select(a => TripAnalysisResultFactory(a, AnalyseTyper.FoersteObsIDK));
        }

        public IQueryable<TripAnalysisResult> FoersteObsIRegion(
            VTur vTur,
            IQueryable<long> arterForTuren)
        {
            return birdWareContext.Art.AsNoTracking()
                .Where(a =>
                    arterForTuren.Contains(a.Id)
                    && !birdWareContext.Observation.AsNoTracking().Any(o =>
                        o.ArtId == a.Id
                        && o.FugleturId < vTur.Id
                        && o.Fugletur.Lokalitet.RegionId == vTur.RegionId))
                .Select(a => TripAnalysisResultFactory(a, AnalyseTyper.FoersteObsIRegion));
        }

        public IQueryable<TripAnalysisResult> FoersteObsForKommune(
            VTur vTur,
            IQueryable<long> arterForTuren)
        {
            return birdWareContext.Art.AsNoTracking()
                .Where(a =>
                    arterForTuren.Contains(a.Id)
                    && !birdWareContext.Observation.AsNoTracking().Any(o =>
                        o.ArtId == a.Id
                        && o.FugleturId < vTur.Id
                        && o.Fugletur.Lokalitet.RegionId == vTur.RegionId
                        && o.Fugletur.Lokalitet.KommuneId == vTur.KommuneId))
                .Select(a => TripAnalysisResultFactory(a, AnalyseTyper.FoersteObsForKommune));
        }

        public IQueryable<TripAnalysisResult> FoersteObsForLokalitet(
            VTur vTur,
            IQueryable<long> arterForTuren)
        {
            return birdWareContext.Art.AsNoTracking()
                .Where(a =>
                    arterForTuren.Contains(a.Id)
                    && !birdWareContext.Observation.AsNoTracking().Any(o =>
                        o.ArtId == a.Id
                        && o.FugleturId < vTur.Id
                        && o.Fugletur.Lokalitet.RegionId == vTur.RegionId
                        && o.Fugletur.LokalitetId == vTur.LokalitetId))
                .Select(a => TripAnalysisResultFactory(a, AnalyseTyper.FoersteObsForLokalitet));
        }

        public IQueryable<TripAnalysisResult> FoersteObsIAar(
                    VTur vTur,
                    IQueryable<long> arterForTuren)
        {
            return birdWareContext.Art.AsNoTracking()
                .Where(a =>
                    arterForTuren.Contains(a.Id)
                    && !birdWareContext.Observation.AsNoTracking().Any(o =>
                        o.ArtId == a.Id
                        && o.FugleturId < vTur.Id
                        && o.Fugletur.Dato.HasValue
                        && o.Fugletur.Dato.Value.Year == vTur.Aarstal
                        && o.Fugletur.Lokalitet.RegionId > 0))
                .Select(a => TripAnalysisResultFactory(a, AnalyseTyper.FoersteObsIAar));
        }

        public IQueryable<TripAnalysisResult> FoersteObsIMaaned(
            VTur vTur,
            IQueryable<long> arterForTuren)
        {
            return birdWareContext.Art.AsNoTracking()
                .Where(a =>
                    arterForTuren.Contains(a.Id)
                    && !birdWareContext.Observation.AsNoTracking().Any(o =>
                        o.ArtId == a.Id
                        && o.FugleturId < vTur.Id
                        && o.Fugletur.Dato.HasValue
                        && o.Fugletur.Dato.Value.Month == vTur.Maaned
                        && o.Fugletur.Lokalitet.RegionId > 0))
                .Select(a => TripAnalysisResultFactory(a, AnalyseTyper.FoersteObsIMaaned));
        }

        private static TripAnalysisResult TripAnalysisResultFactory(Art art, AnalyseTyper analyseType) =>
            new() { 
                AnalyseType = analyseType, 
                ArtId = art.Id,
                ArtNavn = art.Navn ?? string.Empty,
                Speciel = art.Speciel,
                SU = art.SU
            };
    }
}