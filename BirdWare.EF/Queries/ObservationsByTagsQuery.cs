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
                var observationTagFilter = GetFilterForTagType<IObservationTagFilter>(tagType.Key);
                observations = observationTagFilter.Filter(tagType.Value, observations);
            }

            return GenerateResultSet(observations);
        }

        private static List<VObs> GenerateResultSet(IQueryable<Observation> observations)
        {
            return [.. observations
                        .Include(i => i.Fugletur)
                        .Include(i => i.Fugletur.Lokalitet)
                        .Include(i => i.Fugletur.Lokalitet.Region)
                        .Include(i => i.Art)
                        .Include(i => i.Art.Gruppe)
                        .Include(i => i.Art.Gruppe.Familie)
                        .OrderByDescending(r => r.FugleturId)
                        .Take(200)
                        .Select(s => VObs.MapFromObservation(s))];
        }
    }
}