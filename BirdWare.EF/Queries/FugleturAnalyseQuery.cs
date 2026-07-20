using BirdWare.Domain.Entities;
using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BirdWare.EF.Queries
{
    public class FugleturAnalyseQuery(BirdWareContext birdWareContext) : IFugleturAnalyseQuery
    {
        public List<Art> HentArtListe(long fugleturId)
        {
            return [.. birdWareContext.Observation
                .AsNoTracking()
                .Where(o => o.FugleturId == fugleturId)
                .Select(o => new Art { Id = o.ArtId, Navn = o.Art.Navn, Speciel = o.Art.Speciel, SU = o.Art.SU })];
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

        public ILookup<long, FugleturAnalyseData> FindAnalyseData(long fugleturId)
        {
            var artIdList = birdWareContext.Observation
                                .Where(q => q.FugleturId == fugleturId)
                                .Select(s => s.ArtId);

            return birdWareContext.Observation.AsNoTracking()
                .Where(o => o.FugleturId < fugleturId && artIdList.Contains(o.ArtId))
                .Select(o => new FugleturAnalyseData
                {
                    ArtId = o.ArtId,
                    LokalitetId = o.Fugletur.LokalitetId,
                    KommuneId = o.Fugletur.Lokalitet.KommuneId,
                    RegionId = o.Fugletur.Lokalitet.RegionId,
                    Aarstal = o.Fugletur.Aarstal,
                    Maaned = o.Fugletur.Maaned
                })
                .Distinct()
                .ToLookup(l => l.ArtId);
        }
    }
}