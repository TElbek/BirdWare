using BirdWare.Domain.Models;
using BirdWare.Domain.Utilities;
using BirdWare.EF.Interfaces;
using BirdWare.Validation;
using Microsoft.AspNetCore.Mvc;

namespace BirdWare.Controllers
{
    [ApiController]
    public class ObservationSoegController(IObservationsByTagsQuery observationsByTagsQuery) : ControllerBase
    {
        [Route("api/observationer/get/tags")]
        public List<VObs> GetObservationsByTags([FromQuery] string tagListAsJson)
        {
            var tagListAsJSONValidator = new TagListAsJSONValidator();
            var tagListValidator = new TagListValidator();

            if (!tagListAsJSONValidator.Validate(tagListAsJson).IsValid)
            {
                return [];
            }

            var tagList = JsonOperations<Tag>.GetListFromJSON(tagListAsJson);
            if (tagListValidator.Validate(tagList ?? []).IsValid == false)
            {
                return [];
            }

            return observationsByTagsQuery.GetObservationsByTags(tagList ?? []);
        }
    }
}
