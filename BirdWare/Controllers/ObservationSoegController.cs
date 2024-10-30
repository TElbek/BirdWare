using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BirdWare.Controllers
{
    [ApiController]
    public class ObservationSoegController(IObservationsByTagsQuery observationsByTagsQuery) : ControllerBase
    {
        [Route("api/observationer/get/tags")]
        public List<VObs> GetObservationsByTags([FromQuery] string tagListAsJson)
        {
            var tagList = JsonSerializer.Deserialize<List<Tag>>(tagListAsJson);
            return observationsByTagsQuery.GetObservationsByTags(tagList ?? []);
        }
    }
}
