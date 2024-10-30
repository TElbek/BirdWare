using BirdWare.Domain.Models;

namespace BirdWare.EF.Interfaces
{
    public interface IObservationsByTagsQuery
    {
        List<VObs> GetObservationsByTags(List<Tag> TagList);
    }
}
