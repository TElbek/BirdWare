using BirdWare.Domain.Models;

namespace BirdWare.EF.Interfaces
{
    public interface IObservationsByTagsQuery
    {
        IEnumerable<VObs> GetByTags(IEnumerable<Tag> TagList);
    }
}
