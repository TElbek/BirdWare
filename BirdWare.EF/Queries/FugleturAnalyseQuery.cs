using BirdWare.Domain.Entities;
using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BirdWare.EF.Queries
{
    public class FugleturAnalyseQuery(BirdWareContext birdWareContext) : IFugleturAnalyseQuery
    {
        public IEnumerable<TripAnalysisResult> Analyser(long fugleturId)
        {
            var vTur = FindFugletur(fugleturId);
            var artListe = HentArtListe(fugleturId);
            var vObsLookUp = FindAnalyseData(vTur, artListe);

            foreach (var art in artListe)
            {
                if (FoersteObsIDatabasen(vObsLookUp, art))
                    yield return PopulateArtInfo(artListe, TripAnalysisResultFactory(art, AnalyseTyper.FoersteObsIDatabasen));

                if (FoersteObsIRegion(vTur, vObsLookUp, art)) 
                    yield return PopulateArtInfo(artListe, TripAnalysisResultFactory(art, AnalyseTyper.FoersteObsIRegion));

                if (FoersteObsForLokalitet(vTur, vObsLookUp, art)) 
                    yield return PopulateArtInfo(artListe, TripAnalysisResultFactory(art, AnalyseTyper.FoersteObsForLokalitet));

                if (vTur.RegionId > 0)
                {
                    if (FoersteObsIDK(vObsLookUp, art))
                        yield return PopulateArtInfo(artListe, TripAnalysisResultFactory(art, AnalyseTyper.FoersteObsIDK));

                    if(FoersteObsForKommune(vTur, vObsLookUp, art))
                        yield return PopulateArtInfo(artListe, TripAnalysisResultFactory(art, AnalyseTyper.FoersteObsForKommune));

                    if (FoersteObsIAar(vTur, vObsLookUp, art))
                        yield return PopulateArtInfo(artListe, TripAnalysisResultFactory(art, AnalyseTyper.FoersteObsIAar));

                    if (FoersteObsIMaaned(vTur, vObsLookUp, art))
                        yield return PopulateArtInfo(artListe, TripAnalysisResultFactory(art, AnalyseTyper.FoersteObsIMaaned));
                }
            }
        }

        private static TripAnalysisResult PopulateArtInfo(IEnumerable<Art> artListe, TripAnalysisResult analyseResultat)
        {
            var art = artListe.FirstOrDefault(a => a.Id == analyseResultat.ArtId);
            analyseResultat.ArtNavn = art?.Navn ?? string.Empty;
            analyseResultat.Speciel = art?.Speciel ?? false;
            analyseResultat.SU = art?.SU ?? false;
            return analyseResultat;
        }

        private IEnumerable<Art> HentArtListe(long fugleturId)
        {
            return [.. (from o in birdWareContext.Observation.AsNoTracking()
                    join a in birdWareContext.Art.AsNoTracking() on o.ArtId equals a.Id
                    where o.FugleturId == fugleturId
                    select a)];
        }

        private VTur FindFugletur(long fugleturId)
        {
            if (birdWareContext.Fugletur.Any(q => q.Id == fugleturId))
            {
                return (from f in birdWareContext.Fugletur.AsNoTracking()
                        join l in birdWareContext.Lokalitet.AsNoTracking() on f.LokalitetId equals l.Id
                        where f.Id == fugleturId
                        select new VTur
                        {
                            Aarstal = f.Aarstal,
                            Maaned = f.Maaned,
                            Dato = f.Dato,
                            Id = f.Id,
                            LokalitetId = l.Id,
                            KommuneId = l.KommuneId,
                            RegionId = l.RegionId,
                        }).First();
            }
            return new VTur();
        }

        private ILookup<long, VObs> FindAnalyseData(VTur vTur, IEnumerable<Art> artList)
        {
            return (from f in birdWareContext.Fugletur.AsNoTracking()
                    join l in birdWareContext.Lokalitet.AsNoTracking() on f.LokalitetId equals l.Id
                       join o in birdWareContext.Observation.AsNoTracking() on f.Id equals o.FugleturId
                       join a in birdWareContext.Art.AsNoTracking() on o.ArtId equals a.Id
                       where f.Id < vTur.Id && artList.Select(s => s.Id).Contains(a.Id)
                       select new VObs
                       {
                           ArtId = a.Id,
                           LokalitetId = l.Id,
                           KommuneId = l.KommuneId,
                           RegionId = l.RegionId,
                           Aarstal = f.Aarstal,
                           Maaned = f.Maaned,
                       }).Distinct()
                         .ToLookup(l => l.ArtId);
        }

        private static bool FoersteObsIDatabasen(ILookup<long,VObs> vObsLookup, Art art) =>
            !vObsLookup[art.Id].Any();

        private static bool FoersteObsIDK(ILookup<long, VObs> vObsLookup, Art art) => 
            !vObsLookup[art.Id].Any(q => q.RegionId > 0);

        private static bool FoersteObsIRegion(VTur vTur, ILookup<long, VObs> vObsLookup, Art art) => 
            !vObsLookup[art.Id].Any(q => q.RegionId == vTur.RegionId);

        private bool FoersteObsForKommune(VTur vTur, ILookup<long, VObs> vObsLookUp, Art art) => 
            !vObsLookUp[art.Id].Any(q => q.KommuneId == vTur.KommuneId && vTur.KommuneId > 0);

        private static bool FoersteObsForLokalitet(VTur vTur, ILookup<long, VObs> vObsLookup, Art art) =>
            !vObsLookup[art.Id].Any(q => q.LokalitetId == vTur.LokalitetId);

        private static bool FoersteObsIAar(VTur vTur, ILookup<long, VObs> vObsLookup, Art art) =>
            !vObsLookup[art.Id].Any(q => q.Aarstal == vTur.Aarstal && q.RegionId > 0);

        private static bool FoersteObsIMaaned(VTur vTur, ILookup<long, VObs> vObsLookup, Art art) =>
            !vObsLookup[art.Id].Any(q => q.Maaned == vTur.Maaned && q.RegionId > 0);

        private static TripAnalysisResult TripAnalysisResultFactory(Art art, AnalyseTyper analyseType)
        {
            return new TripAnalysisResult { AnalyseType = analyseType, ArtId = art.Id };
        }
    }
}