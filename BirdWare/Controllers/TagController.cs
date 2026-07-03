using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using BirdWare.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BirdWare.Controllers
{
    [ApiController]
    public class TagController(ITagHandler tagHandler,
                               IArtQueries artQueries,
                               ISoegArtIkkeSetPaaTurHandler soegArtIkkeSetPaaTurHandler) : ControllerBase
    {
        [HttpGet]
        [Route("api/tags")]
        public List<TagGroup> GetTagList([FromQuery] string query)
        {
            return tagHandler.GetTagList(query);
        }

        [HttpGet]
        [Route("api/tags/fugletur")]
        public List<TagGroup> GetTagListFugletur([FromQuery] string query)
        {
            return tagHandler.GetTagListFugletur(query);
        }

        [Route("api/tag/{query}")]
        public Tag GetTag(string query)
        {
            return tagHandler.GetTag(query);
        }

        [Route("api/tags/arter")]
        public List<Tag> GetTagsArter([FromQuery] string query)
        {
            return soegArtIkkeSetPaaTurHandler.GetTags(query);
        }

        [Route("api/tag/art/{Id}")]
        public Tag GetArtTagById(long Id)
        {
            return artQueries.GetArtTagById(Id);
        }

        [Route("api/tags/familie")]
        public List<Tag> GetFamilieTagsBySearchValue([FromQuery] string query)
        {
            return tagHandler.GetFamilieTagsBySearchValue(query);
        }

        [Route("api/tags/tagsFromNames")]
        public string GetTagsFromNamesAsJSON([FromQuery] string tagNamesAsJson)
        {
            return tagHandler.GetTagsFromNamesAsJSON(tagNamesAsJson);
        }
    }
}