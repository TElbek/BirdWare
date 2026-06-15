using BirdWare.Domain.Entities;
using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BirdWare.EF.Queries
{
    public class ObservationsByTagsQuery(
                    BirdWareContext birdWareContext, IServiceProvider serviceProvider) : 
                    BaseByTagsQuery(birdWareContext, serviceProvider), IObservationsByTagsQuery
    {
        public List<VObs> GetObservationsByTags(List<Tag> tagList)
        {
            var observations = birdWareContext.Observation.AsNoTracking();

            foreach (var tagType in TagsGroupedByTagType(tagList))
            {
                var filterImplementation = GetFilterForTagType<IObservationTagFilter>(tagType.Key);
                var result = filterImplementation?.Filter(tagType.Value, observations);
                if (result is IQueryable<Observation> observationResult) 
                {
                    observations = observationResult;
                }
            }

            return GenerateResultSet(observations);
        }

        private List<VObs> GenerateResultSet(IQueryable<Observation> observations)
        {
            var result = (from o in observations
                          join f in birdWareContext.Fugletur.AsNoTracking() on o.FugleturId equals f.Id
                          join l in birdWareContext.Lokalitet.AsNoTracking() on f.LokalitetId equals l.Id
                          join kgrp in birdWareContext.Kommune.AsNoTracking() on l.KommuneId equals kgrp.Id into kgroup
                          from k in kgroup.DefaultIfEmpty()
                          join r in birdWareContext.Region.AsNoTracking() on l.RegionId equals r.Id
                          join a in birdWareContext.Art.AsNoTracking() on o.ArtId equals a.Id
                          join g in birdWareContext.Gruppe.AsNoTracking() on a.GruppeId equals g.Id
                          join fa in birdWareContext.Familie.AsNoTracking() on g.FamilieId equals fa.Id
                          orderby f.Dato descending
                          select VObs.MapToVObs(o, f, l, k, r, a, g, fa)).Take(200).ToList();

            return result;
        }        
    }
}