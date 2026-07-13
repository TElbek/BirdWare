using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BirdWare.EF.Queries
{
    public abstract class BaseByTagsQuery(BirdWareContext birdWareContext, IServiceProvider serviceProvider)
    {
        protected readonly BirdWareContext birdWareContext = birdWareContext;

        protected static bool ErSaesonTagType(Tag tag) => SaesonMaaneder.Liste.Any(q => q.Key == tag.TagType);

        protected T? GetFilterForTagType<T>(TagTypes tagType) where T : class, ITagFilter
        {
            try
            {
                return serviceProvider.GetRequiredKeyedService<T>(tagType);
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }

        internal static IEnumerable<Tag> TransformSaesonTagsToMonthTags(List<Tag> tagList)
        {
            var seasonTags = tagList.Where(ErSaesonTagType).ToList();
            foreach (var seasonTag in seasonTags)
            {
                tagList.Remove(seasonTag);
                var saesonMaaned = SaesonMaaneder.Liste.First(q => q.Key == seasonTag.TagType);
                saesonMaaned.Value.ToList().ForEach(maaned =>
                {
                    tagList.Add(new Tag
                    {
                        Id = maaned,
                        Name = maaned.ToString(),
                        TagType = TagTypes.Maaned
                    });
                });
            }
            return tagList;
        }

        protected static Dictionary<TagTypes, List<Tag>> TagsGroupedByTagType(IEnumerable<Tag> tagList)
        {
            return TransformSaesonTagsToMonthTags([.. tagList])
                    .GroupBy(t => t.TagType).ToDictionary(g => g.Key, g => g.ToList());
        }
    }
}