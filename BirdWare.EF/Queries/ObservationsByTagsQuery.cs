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
            var result = (from obs in observations
                          join tur in birdWareContext.Fugletur.AsNoTracking() on obs.FugleturId equals tur.Id
                          join lok in birdWareContext.Lokalitet.AsNoTracking() on tur.LokalitetId equals lok.Id
                          join kgrp in birdWareContext.Kommune.AsNoTracking() on lok.KommuneId equals kgrp.Id into kgroup
                          from kom in kgroup.DefaultIfEmpty()
                          join reg in birdWareContext.Region.AsNoTracking() on lok.RegionId equals reg.Id
                          join art in birdWareContext.Art.AsNoTracking() on obs.ArtId equals art.Id
                          join grp in birdWareContext.Gruppe.AsNoTracking() on art.GruppeId equals grp.Id
                          join fam in birdWareContext.Familie.AsNoTracking() on grp.FamilieId equals fam.Id
                          orderby tur.Dato descending
                          select VObs.MapToVObs(obs, tur, lok, kom, reg, art, grp, fam)).Take(200).ToList();

            return result;
        }        
    }
}