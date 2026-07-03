using BirdWare.Domain.Models;
using BirdWare.Domain.Utilities;
using BirdWare.EF.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BirdWare.Controllers
{
    [ApiController]
    public class ObservationSoegController(IObservationsByTagsQuery observationsByTagsQuery) : ControllerBase
    {
        [Route("api/observationer/get/tags")]
        public List<VObs> GetObservationsByTags([FromQuery] string tagListAsJson)
        {
            var tagList = JsonOperations<Tag>.GetListFromJSON(tagListAsJson);
            return observationsByTagsQuery.GetObservationsByTags(tagList ?? []);
        }
    }
}
