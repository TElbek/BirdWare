using BirdWare.Domain.Models;

namespace BirdWare.EF.Interfaces
{
    public interface ITagQuery
    {
        IEnumerable<Tag> GetTagList();
        IEnumerable<Tag> GetTagListFugletur();
        IEnumerable<Tag> GetTagListSpecies();
    }
}
