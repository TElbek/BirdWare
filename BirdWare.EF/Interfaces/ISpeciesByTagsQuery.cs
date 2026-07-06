using BirdWare.Domain.Models;

namespace BirdWare.EF.Interfaces
{
    public interface ISpeciesByTagsQuery
    {
        List<VArt> GetByTags(List<Tag> TagList);
    }
}
