using BirdWare.Domain.Models;

namespace BirdWare.EF.Interfaces
{
    public interface ITagQuery
    {
        List<Tag> GetTagList();
        List<Tag> GetTagListFugletur();
        Tag GetArtTagById(short Id);
    }
}
