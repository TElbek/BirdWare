using BirdWare.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace BirdWare.Interfaces
{
    public interface ITagHandler
    {
        IEnumerable<TagGroup> GetTagList(string query);
        IEnumerable<TagGroup> GetTagListFugletur(string query);
        IEnumerable<TagGroup> GetTagListArter(string query);
        IEnumerable<Tag> GetTagListArt(string query);
        IEnumerable<Tag> GetFamilieTagsBySearchValue([FromQuery] string query);
        string GetTagsFromNamesAsJSON([FromQuery] string tagNamesAsJson);
        Tag GetTag(string query);
    }
}
