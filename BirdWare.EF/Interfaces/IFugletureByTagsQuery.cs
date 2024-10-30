using BirdWare.Domain.Models;

namespace BirdWare.EF.Interfaces
{
    public interface IFugletureByTagsQuery
    {
        List<VTur> GetFugletureByTags(List<Tag> TagList);
    }
}
