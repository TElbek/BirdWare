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

        internal static List<Tag> TransformSaesonTagsToMonthTags(List<Tag> tagList)
        {
            var result = new List<Tag>();    

            foreach (var tag in tagList)
            {
                if(ErSaesonTagType(tag))
                {
                    var saesonMaaned = SaesonMaaneder.Liste.First(q => q.Key == tag.TagType);
                    saesonMaaned.Value.ForEach(maaned =>
                    {
                        result.Add(new Tag
                        {
                            Id = maaned,
                            Name = maaned.ToString(),
                            ParentId = tag.ParentId,
                            TagType = TagTypes.Maaned,
                            TagTypeTitle = "Måned"
                        });
                    });
                }
                else
                {
                    result.Add(tag);
                }
            }
            return result;
        }

        protected static Dictionary<TagTypes, List<Tag>> TagsGroupedByTagType(List<Tag> tagList)
        {
            return TransformSaesonTagsToMonthTags(tagList)
                    .GroupBy(t => t.TagType).ToDictionary(g => g.Key, g => g.ToList());
        }
    }
}