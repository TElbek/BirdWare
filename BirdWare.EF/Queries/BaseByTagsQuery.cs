using BirdWare.Domain.Models;

namespace BirdWare.EF.Queries
{
    public abstract class BaseByTagsQuery(BirdWareContext birdWareContext)
    {
        protected readonly BirdWareContext birdWareContext = birdWareContext;

        protected static bool ErSaesonTagType(Tag tag) => SaesonMaaneder.Liste.Any(q => q.Key == tag.TagType);
    }
}