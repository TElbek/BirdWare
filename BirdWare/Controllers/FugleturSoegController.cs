using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BirdWare.Controllers
{
    [ApiController]
    public class FugleturSoegController(IFugletureByTagsQuery fugletureByTagsQuery) : ControllerBase
    {
        [Route("api/fugleture/get/tags")]
        public List<VTur> GetFugletureByTags([FromQuery] string tagListAsJson)
        {
            var tagList = JsonSerializer.Deserialize<List<Tag>>(tagListAsJson);
            return fugletureByTagsQuery.GetFugletureByTags(tagList ?? []);
        }
    }
}
