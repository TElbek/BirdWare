using BirdWare.Domain.Models;

namespace BirdWare.EF.Interfaces
{
    public interface IArtByTagsQuery
    {
        IEnumerable<VArt> GetByTags(IEnumerable<Tag> TagList);
    }
}
