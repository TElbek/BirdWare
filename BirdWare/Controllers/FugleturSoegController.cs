using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using System.Text.Json;

namespace BirdWare.Controllers
{
    [ApiController]
    public class FugleturSoegController(
        IFugletureByTagsQuery fugletureByTagsQuery,
        IFugleturQuery fugleturQuery) : ControllerBase
    {
        [OutputCache]
        [Route("api/fugleture/get/tags")]
        public List<VTur> GetFugletureByTags([FromQuery] string tagListAsJson)
        {
            var tagList = JsonSerializer.Deserialize<List<Tag>>(tagListAsJson);
            return fugletureByTagsQuery.GetFugletureByTags(tagList ?? []);
        }

        [Route("api/fugleture/aar/maaned")]
        public List<VTur> GetFugletureAarMaaned([FromQuery] long aarstal, long maaned)
        {
            return fugleturQuery.GetFugleTureAarMaaned(aarstal,maaned);
        }
    }
}
