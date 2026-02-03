using BirdWare.Domain.Models;

namespace BirdWare.EF.TagFilters
{
    public abstract class BaseTagFilter<T> where T : class
    {
        public abstract IQueryable<T> Filter(List<Tag> tagList, IQueryable<T> queryable);
        protected static bool ErSaesonTagType(Tag tag) => SaesonMaaneder.Liste.Any(q => q.Key == tag.TagType);

        protected static List<long> GetTagIds(List<Tag> TagList)
        {
            return [.. TagList.Select(s => s.Id)];
        }
    }
}
