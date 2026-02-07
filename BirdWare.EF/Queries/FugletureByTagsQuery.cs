using BirdWare.Domain.Entities;
using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BirdWare.EF.Queries
{
    public class FugletureByTagsQuery(
                    BirdWareContext birdWareContext, IServiceProvider serviceProvider) : 
                    BaseByTagsQuery(birdWareContext, serviceProvider), IFugletureByTagsQuery
    {        
        public List<VTur> GetFugletureByTags(List<Tag> tagList)
        {
            var fugleture = birdWareContext.Fugletur.AsNoTracking();

            foreach (var tagType in TagsGroupedByTagType(tagList))
            {
                var fugleturTagFilter = GetFilterForTagType<IFugleturTagFilter>(tagType.Key);
                var result = fugleturTagFilter.Filter(tagType.Value, fugleture);
                if (result is IQueryable<Fugletur> fugleturResult)
                {
                    fugleture = fugleturResult;
                }
            };

            return GenerateResultSet(fugleture);
        }

        private static List<VTur> GenerateResultSet(IQueryable<Fugletur> fugleture)
        {
            var listeAfFugleture = fugleture
                    .Include(i => i.Lokalitet)
                    .Include(i => i.Lokalitet.Region)
                    .OrderByDescending(r => r.Id)
                    .Take(30)
                    .ToList();

            return [.. listeAfFugleture.Select(s => VTur.MapFromFugletur(s))];
        }
    }
}