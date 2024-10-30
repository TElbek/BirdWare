using BirdWare.Domain.Entities;
using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BirdWare.EF.Queries
{
    public class FugleturAnalyseQuery(IDbContextFactory<BirdWareContext> dbContextFactory, IFugleturQuery fugleturQuery) : IFugleturAnalyseQuery
    {
        public List<FugleturAnalyse> Analyser(long fugleturId, AnalyseTyper analyseType)
        {
            var resultList = new List<FugleturAnalyse>();
            var fugletur = fugleturQuery.GetFugletur(fugleturId);
            var arterPaaDenneTur = FindArterPaaDenneTur(fugletur);
            var analysisMethods = HentAnalyseMetoder();

            var metodeForAnalyseType = analysisMethods.FirstOrDefault(f => ErMetodeForAnalyseTypeOgRegion(analyseType, f, fugletur.RegionId));
            if (metodeForAnalyseType != null)
            {
                Parallel.ForEach(arterPaaDenneTur, art =>
                {
                    if (metodeForAnalyseType.Invoke(this, [fugletur, art]) is bool result && result)
                    {
                        resultList.Add(FugleturAnalyseFactory(fugletur, art, analyseType));
                    }
                });
            }
            return [.. resultList.OrderBy(o => o.AnalyseType).ThenBy(p => p.ArtNavn)];
        }

        private MethodInfo[] HentAnalyseMetoder()
        {
            return GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);
        }

        private static bool ErMetodeForAnalyseTypeOgRegion(AnalyseTyper analyseType, MethodInfo method, long regionId)
        {
            return method.GetCustomAttributes(false)
                         .Where(q => q.GetType() == typeof(AnalyseTypeAttribute))
                         .Cast<AnalyseTypeAttribute>()
                         .Any(q => q.AnalyseType == analyseType && !(regionId < 0 && q.KunIndland == true));
        }

        [AnalyseType(AnalyseTyper.FoersteObsIDatabasen, false)]
        private bool NyDatabaseArt(VTur fugletur, Art species)
        {
            using var birdWareContext = dbContextFactory.CreateDbContext();
            return !(from o in birdWareContext.Observation
                     where o.FugleturId < fugletur.Id &&
                           o.ArtId == species.Id
                     select o.Id).Any();
        }

        [AnalyseType(AnalyseTyper.FoersteObsIDK, true)]
        private bool NyDKArt(VTur fugletur, Art species)
        {
            using var birdWareContext = dbContextFactory.CreateDbContext();
            return !(from o in birdWareContext.Observation join
                          f in birdWareContext.Fugletur on o.FugleturId equals f.Id join
                          l in birdWareContext.Lokalitet on f.LokalitetId equals l.Id
                     where f.Id < fugletur.Id &&
                           o.ArtId == species.Id &&
                           l.RegionId > 0
                     select o.Id).Any();
        }

        [AnalyseType(AnalyseTyper.FoersteObsIRegion, false)]
        private bool NyRegionArt(VTur fugletur, Art species)
        {
            using var birdWareContext = dbContextFactory.CreateDbContext();
            return !(from o in birdWareContext.Observation join
                          f in birdWareContext.Fugletur on o.FugleturId equals f.Id join
                          l in birdWareContext.Lokalitet on f.LokalitetId equals l.Id
                     where f.Id < fugletur.Id &&
                           o.ArtId == species.Id &&
                           l.RegionId == fugletur.RegionId
                     select o.Id).Any();
        }

        [AnalyseType(AnalyseTyper.FoersteObsForLokalitet, false)]
        private bool NyLokalitetArt(VTur fugletur, Art species)
        {
            using var birdWareContext = dbContextFactory.CreateDbContext();
            return !(from o in birdWareContext.Observation join
                          f in birdWareContext.Fugletur on o.FugleturId equals f.Id
                     where f.Id < fugletur.Id &&
                           o.ArtId == species.Id &&
                           f.LokalitetId == fugletur.LokalitetId
                     select o.Id).Any();
        }

        [AnalyseType(AnalyseTyper.FoersteObsIMaaned, true)]
        private bool NyMaanedArt(VTur fugletur, Art species)
        {
            using var birdWareContext = dbContextFactory.CreateDbContext();

            var withMaaned = birdWareContext.Fugletur.GetAarMaaned()
                                                     .Where(a => a.Maaned == fugletur.Maaned);

            return !(from o in birdWareContext.Observation
                     where o.FugleturId < fugletur.Id &&
                           o.ArtId == species.Id &&
                           withMaaned.Any(q => q.FugleturId == o.FugleturId)
                     select o.Id).Any();
        }

        [AnalyseType(AnalyseTyper.FoersteObsIAar, true)]
        private bool NyAarsArt(VTur fugletur, Art species)
        {
            using var birdWareContext = dbContextFactory.CreateDbContext();

            var withAarstal = birdWareContext.Fugletur.GetAarMaaned()
                                                     .Where(a => a.Aarstal == fugletur.Aarstal);

            return !(from o in birdWareContext.Observation join
                          f in birdWareContext.Fugletur on o.FugleturId equals f.Id join
                          l in birdWareContext.Lokalitet on f.LokalitetId equals l.Id
                     where o.FugleturId < fugletur.Id &&
                           o.ArtId == species.Id &&
                           withAarstal.Any(q => q.FugleturId == o.FugleturId) &&
                           l.RegionId > 0
                     select o.Id).Any();
        }

        private List<Art> FindArterPaaDenneTur(VTur fugletur)
        {
            using var birdWareContext = dbContextFactory.CreateDbContext();
            return [.. (from o in birdWareContext.Observation
                    join a in birdWareContext.Art on o.ArtId equals a.Id
                    where o.FugleturId == fugletur.Id
                    select new Art { Id = a.Id, Navn = a.Navn, SU = a.SU })];
        }

        private static FugleturAnalyse FugleturAnalyseFactory(VTur fugletur, Art species, AnalyseTyper analyseType) => new()
        {
            ArtId = species.Id,
            AnalyseType = analyseType,
            ArtNavn = species.Navn ?? string.Empty,
            LokalitetId = fugletur.LokalitetId,
            SU = species.SU
        };

    }
}