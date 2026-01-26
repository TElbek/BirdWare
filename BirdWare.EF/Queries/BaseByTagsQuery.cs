using BirdWare.Domain.Models;
using System.Reflection;

namespace BirdWare.EF.Queries
{
    public abstract class BaseByTagsQuery(BirdWareContext birdWareContext)
    {
        protected readonly BirdWareContext birdWareContext = birdWareContext;

        protected static bool ErSaesonTagType(Tag tag) => SaesonMaaneder.Liste.Any(q => q.Key == tag.TagType);

        protected MethodInfo[] HentFilterMetoder()
        {
            return GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
        }

        protected static MethodInfo? FindFilterMetodeForTagType(TagTypes tagType, MethodInfo[] methodInfoList)
        {
            var allMethodsWithAttribute = methodInfoList
                            .Select(b => new { attr = b.GetCustomAttribute<TagFilterAttribute>(), method = b });

            var methodAttribute = allMethodsWithAttribute
               .FirstOrDefault(q => q.attr != null && (q.attr.TagType == tagType || q.attr.TagTypeList.Contains(tagType)));

            return methodAttribute?.method;
        }

        protected static List<long> GetTagIdsByType(List<Tag> TagList, TagTypes tagType)
        {
            return [.. TagList
                            .Where(r => r.TagType == tagType)
                            .Select(s => s.Id)
                            .Cast<long>()];
        }
    }
}
