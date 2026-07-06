using BirdWare.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace BirdWare.Interfaces
{
    public interface ITagHandler
    {
        List<TagGroup> GetTagList(string query);
        List<TagGroup> GetTagListFugletur(string query);
        List<TagGroup> GetTagListArter(string query);
        List<Tag> GetTagListArt(string query);
        List<Tag> GetFamilieTagsBySearchValue([FromQuery] string query);
        string GetTagsFromNamesAsJSON([FromQuery] string tagNamesAsJson);
        Tag GetTag(string query);
    }
}
