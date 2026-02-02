using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BirdWare.EF.Queries
{
    public abstract class BaseByTagsQuery(BirdWareContext birdWareContext, IServiceProvider serviceProvider)
    {
        protected readonly BirdWareContext birdWareContext = birdWareContext;

        protected static bool ErSaesonTagType(Tag tag) => SaesonMaaneder.Liste.Any(q => q.Key == tag.TagType);

        protected T GetFilterForTagType<T>(TagTypes tagType) where T : class, ITagFilter
        {
            return serviceProvider.GetRequiredKeyedService<T>(tagType);
        }

        protected static Dictionary<TagTypes, List<Tag>> GetGroupByTagTypes(List<Tag> tagList) =>
            tagList.GroupBy(t => t.TagType).ToDictionary(g => g.Key, g => g.ToList());
    }
}