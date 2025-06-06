﻿using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using BirdWare.Domain.Entities;

namespace BirdWare.EF.Queries
{
    public class FugleturQuery(BirdWareContext birdWareContext) : IFugleturQuery
    {
        public List<VTur> GetFugleTureAarMaaned(long aarstal, long maaned)
        {
            var withMaaned = birdWareContext.Fugletur.GetAarMaaned()
                            .Where(q => q.Aarstal == aarstal && q.Maaned == maaned);

            return [.. (from f in birdWareContext.Fugletur join
                             l in birdWareContext.Lokalitet on f.LokalitetId equals l.Id join
                             r in birdWareContext.Region on l.RegionId equals r.Id
                    where withMaaned.Any(s => s.FugleturId == f.Id)
                    select new VTur
                    {
                        Aarstal = f.Aarstal,
                        Maaned = f.Maaned,
                        Dato = f.Dato,
                        Id = f.Id,
                        LokalitetId = l.Id,
                        LokalitetNavn = l.Navn ?? string.Empty,
                        RegionId = r.Id,
                        RegionNavn = r.Navn ?? string.Empty
                    })];
        }

        public VTur GetFugletur(long id)
        {
            var vtur = from f in birdWareContext.Fugletur
                       join
                            l in birdWareContext.Lokalitet on f.LokalitetId equals l.Id
                       join
                            r in birdWareContext.Region on l.RegionId equals r.Id
                       where f.Id == id
                       select new VTur{
                           Aarstal = f.Aarstal,
                           Maaned = f.Maaned,
                           Dato = f.Dato,
                           Id = f.Id,
                           LokalitetId = l.Id,
                           LokalitetNavn = l.Navn ?? string.Empty,
                           RegionId = r.Id,
                           RegionNavn = r.Navn ?? string.Empty,
                           AntalArter = birdWareContext.Observation
                                 .Where(o => o.FugleturId == f.Id)
                                 .Select(o => o.ArtId)
                                 .Count()
                       };

            return vtur.Any() ? vtur.First() : new VTur();
        }

        public long GetSenesteFugletur()
        {
            return birdWareContext.Fugletur.Max(f => f.Id);
        }
    }
}
