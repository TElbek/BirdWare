using BirdWare.Domain.Models;

namespace BirdWare.EF.Interfaces
{
    public interface IFugletureByTagsQuery
    {
        IEnumerable<VTur> GetFugletureByTags(IEnumerable<Tag> TagList);
    }
}
