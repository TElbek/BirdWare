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
        public IEnumerable<TagGroup> GetTagList([FromQuery] string query)
        {
            return tagHandler.GetTagList(query);
        }

        [HttpGet]
        [Route("api/tags/arter")]
        public IEnumerable<TagGroup> GetTagListArter([FromQuery] string query)
        {
            return tagHandler.GetTagListArter(query);
        }

        [HttpGet]
        [Route("api/tags/fugletur")]
        public IEnumerable<TagGroup> GetTagListFugletur([FromQuery] string query)
        {
            return tagHandler.GetTagListFugletur(query);
        }


        [Route("api/tag/{query}")]
        public Tag GetTag(string query)
        {
            return tagHandler.GetTag(query);
        }

        [Route("api/tags/arter/ikke-set-paa-tur")]
        public IEnumerable<Tag> GetTagsArterIkkeSetPaaTur([FromQuery] string query)
        {
            return soegArtIkkeSetPaaTurHandler.Handle(query);
        }

        [Route("api/tag/art/{Id}")]
        public Tag GetArtTagById(long Id)
        {
            return artQueries.GetArtTagById(Id);
        }

        [Route("api/tags/familie")]
        public IEnumerable<Tag> GetFamilieTagsBySearchValue([FromQuery] string query)
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