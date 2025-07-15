using BirdWare.Domain.Entities;
using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;

namespace BirdWare.EF.Queries
{
    public class FugleturAnalyseQuery(BirdWareContext birdWareContext) : IFugleturAnalyseQuery
    {
        public List<TripAnalysisResult> Analyser(long fugleturId)
        {
            var vTur = FindFugletur(fugleturId);
            var artListe = HentArtListe(fugleturId);
            var artIdList = artListe.Select(x => x.Id).ToList();

            var analyseResultatListe = new List<TripAnalysisResult>();

            var vObsList = FindAnalyseData(vTur, artIdList);

            foreach(var artId in artIdList)
            {
                if (FoersteObsIDatabasen(vObsList, artId)) 
                    analyseResultatListe.Add(TripAnalysisResultFactory(artId, AnalyseTyper.FoersteObsIDatabasen));

                if (FoersteObsIDK(vObsList, artId)) 
                    analyseResultatListe.Add(TripAnalysisResultFactory(artId, AnalyseTyper.FoersteObsIDK));

                if (FoersteObsIRegion(vTur, vObsList, artId)) 
                    analyseResultatListe.Add(TripAnalysisResultFactory(artId, AnalyseTyper.FoersteObsIRegion));

                if (FoersteObsForLokalitet(vTur, vObsList, artId)) 
                    analyseResultatListe.Add(TripAnalysisResultFactory(artId, AnalyseTyper.FoersteObsForLokalitet));

                if (FoersteObsIAar(vTur, vObsList, artId)) 
                    analyseResultatListe.Add(TripAnalysisResultFactory(artId, AnalyseTyper.FoersteObsIAar));

                if (FoersteObsIMaaned(vTur, vObsList, artId)) 
                    analyseResultatListe.Add(TripAnalysisResultFactory(artId, AnalyseTyper.FoersteObsIMaaned));
            }

            Parallel.ForEach(analyseResultatListe, analyseResultat =>
            {
                var art = artListe.FirstOrDefault(a => a.Id == analyseResultat.ArtId);

                analyseResultat.ArtNavn = art?.Navn ?? string.Empty;
                analyseResultat.Speciel = art?.Speciel ?? false;
                analyseResultat.SU = art?.SU ?? false;
            });

            return [.. analyseResultatListe.OrderBy(o => o.AnalyseType)];
        }

        private List<Art> HentArtListe(long fugleturId)
        {
            return [.. (from o in birdWareContext.Observation
                    join a in birdWareContext.Art on o.ArtId equals a.Id
                    where o.FugleturId == fugleturId
                    select a)];
        }

        private VTur FindFugletur(long fugleturId)
        {
            return (from f in birdWareContext.Fugletur
                    join l in birdWareContext.Lokalitet on f.LokalitetId equals l.Id
                    where f.Id == fugleturId
                    select new VTur
                    {
                        Aarstal = f.Aarstal,
                        Maaned = f.Maaned,
                        Dato = f.Dato,
                        Id = f.Id,
                        LokalitetId = l.Id,
                        RegionId = l.RegionId,
                    }).First();
        }

        private List<VObs> FindAnalyseData(VTur vTur, List<long> artIdList)
        {
            return [.. (from f in birdWareContext.Fugletur
                       join l in birdWareContext.Lokalitet on f.LokalitetId equals l.Id
                       join o in birdWareContext.Observation on f.Id equals o.FugleturId
                       join a in birdWareContext.Art on o.ArtId equals a.Id
                       where f.Id < vTur.Id && artIdList.Contains(a.Id)
                       select new VObs
                       {
                           ArtId = a.Id,
                           LokalitetId = l.Id,
                           RegionId = l.RegionId,
                           Aarstal = f.Aarstal,
                           Maaned = f.Maaned,
                       }).Distinct()];
        }

        private static bool FoersteObsIDatabasen(List<VObs> vObsList, long artId) => 
            !vObsList.Any(q => q.ArtId == artId);

        private static bool FoersteObsIDK(List<VObs> vObsList, long artId) => 
            !vObsList.Any(q => q.ArtId == artId && q.RegionId > 0);

        private static bool FoersteObsIRegion(VTur vTur, List<VObs> vObsList, long artId) => 
            !vObsList.Any(q => q.ArtId == artId && q.RegionId == vTur.RegionId);

        private static bool FoersteObsForLokalitet(VTur vTur, List<VObs> vObsList, long artId) =>
            !vObsList.Any(q => q.ArtId == artId && q.LokalitetId == vTur.LokalitetId);

        private static bool FoersteObsIAar(VTur vTur, List<VObs> vObsList, long artId) =>
            !vObsList.Any(q => q.ArtId == artId && q.Aarstal == vTur.Aarstal && q.RegionId > 0);

        private static bool FoersteObsIMaaned(VTur vTur, List<VObs> vObsList, long artId) =>
            !vObsList.Any(q => q.ArtId == artId && q.Maaned == vTur.Maaned && q.RegionId > 0);

        private static TripAnalysisResult TripAnalysisResultFactory(long artId, AnalyseTyper analyseType)
        {
            return new TripAnalysisResult { AnalyseType = analyseType, ArtId = artId };
        }
    }
}