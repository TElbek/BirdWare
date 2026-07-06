using BirdWare.Domain.Models;

namespace BirdWare.EF.Interfaces
{
    public interface IArtByTagsQuery
    {
        List<VArt> GetByTags(List<Tag> TagList);
    }
}
