using BirdWare.Domain.Models;

namespace BirdWare.EF.Interfaces
{
    public interface IObservationsByTagsQuery
    {
        IEnumerable<VObs> GetByTags(List<Tag> TagList);
    }
}
