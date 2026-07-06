using BirdWare.Domain.Entities;
using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BirdWare.EF.Queries
{
    public abstract class ObservationsByTagsQueryBase<T>(
                    BirdWareContext birdWareContext, IServiceProvider serviceProvider) :
                    BaseByTagsQuery(birdWareContext, serviceProvider) where T : class
    {
        protected abstract List<T> GenerateResultSet(IQueryable<Observation> observations);
        
        public List<T> GetByTags(List<Tag> tagList)
        {
            IQueryable<Observation> observations = GetData(tagList);
            return GenerateResultSet(observations);
        }

        private IQueryable<Observation> GetData(List<Tag> tagList)
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
            return observations;
        }        
    }
}
