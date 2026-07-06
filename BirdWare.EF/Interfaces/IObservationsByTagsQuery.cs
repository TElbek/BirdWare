using BirdWare.Domain.Models;

namespace BirdWare.EF.Interfaces
{
    public interface IObservationsByTagsQuery
    {
        List<VObs> GetByTags(List<Tag> TagList);
    }
}
