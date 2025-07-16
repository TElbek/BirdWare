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

            var analyseResultatListe = new List<TripAnalysisResult>();

            var vObsList = FindAnalyseData(vTur, artListe);

            foreach(var art in artListe)
            {
                if (FoersteObsIDatabasen(vObsList, art)) 
                    analyseResultatListe.Add(TripAnalysisResultFactory(art, AnalyseTyper.FoersteObsIDatabasen));

                if (FoersteObsIRegion(vTur, vObsList, art)) 
                    analyseResultatListe.Add(TripAnalysisResultFactory(art, AnalyseTyper.FoersteObsIRegion));

                if (FoersteObsForLokalitet(vTur, vObsList, art)) 
                    analyseResultatListe.Add(TripAnalysisResultFactory(art, AnalyseTyper.FoersteObsForLokalitet));

                if (vTur.RegionId > 0)
                {
                    if (FoersteObsIDK(vObsList, art))
                        analyseResultatListe.Add(TripAnalysisResultFactory(art, AnalyseTyper.FoersteObsIDK));

                    if (FoersteObsIAar(vTur, vObsList, art))
                        analyseResultatListe.Add(TripAnalysisResultFactory(art, AnalyseTyper.FoersteObsIAar));

                    if (FoersteObsIMaaned(vTur, vObsList, art))
                        analyseResultatListe.Add(TripAnalysisResultFactory(art, AnalyseTyper.FoersteObsIMaaned));
                }
            }

            Parallel.ForEach(analyseResultatListe, analyseResultat =>
            {
                var art = artListe.FirstOrDefault(a => a.Id == analyseResultat.ArtId);

                analyseResultat.ArtNavn = art?.Navn ?? string.Empty;
                analyseResultat.Speciel = art?.Speciel ?? false;
                analyseResultat.SU = art?.SU ?? false;
            });

            return [.. analyseResultatListe
                            .OrderBy(o => o.AnalyseType)
                            .ThenBy(o => o.ArtNavn)];
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

        private List<VObs> FindAnalyseData(VTur vTur, List<Art> artList)
        {
            return [.. (from f in birdWareContext.Fugletur
                       join l in birdWareContext.Lokalitet on f.LokalitetId equals l.Id
                       join o in birdWareContext.Observation on f.Id equals o.FugleturId
                       join a in birdWareContext.Art on o.ArtId equals a.Id
                       where f.Id < vTur.Id && artList.Select(s => s.Id).Contains(a.Id)
                       select new VObs
                       {
                           ArtId = a.Id,
                           LokalitetId = l.Id,
                           RegionId = l.RegionId,
                           Aarstal = f.Aarstal,
                           Maaned = f.Maaned,
                       }).Distinct()];
        }

        private static bool FoersteObsIDatabasen(List<VObs> vObsList, Art art) => 
            !vObsList.Any(q => q.ArtId == art.Id);

        private static bool FoersteObsIDK(List<VObs> vObsList, Art art) => 
            !vObsList.Any(q => q.ArtId == art.Id && q.RegionId > 0);

        private static bool FoersteObsIRegion(VTur vTur, List<VObs> vObsList, Art art) => 
            !vObsList.Any(q => q.ArtId == art.Id && q.RegionId == vTur.RegionId);

        private static bool FoersteObsForLokalitet(VTur vTur, List<VObs> vObsList, Art art) =>
            !vObsList.Any(q => q.ArtId == art.Id && q.LokalitetId == vTur.LokalitetId);

        private static bool FoersteObsIAar(VTur vTur, List<VObs> vObsList, Art art) =>
            !vObsList.Any(q => q.ArtId == art.Id && q.Aarstal == vTur.Aarstal && q.RegionId > 0);

        private static bool FoersteObsIMaaned(VTur vTur, List<VObs> vObsList, Art art) =>
            !vObsList.Any(q => q.ArtId == art.Id && q.Maaned == vTur.Maaned && q.RegionId > 0);

        private static TripAnalysisResult TripAnalysisResultFactory(Art art, AnalyseTyper analyseType)
        {
            return new TripAnalysisResult { AnalyseType = analyseType, ArtId = art.Id };
        }
    }
}