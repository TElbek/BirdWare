using BirdWare.Domain.Models;
using BirdWare.Domain.Utilities;
using BirdWare.EF.Interfaces;
using BirdWare.Validation;
using Microsoft.AspNetCore.Mvc;

namespace BirdWare.Controllers
{
    public class ArtSoegController(ISpeciesByTagsQuery speciesByTagsQuery) : ControllerBase
    {
        [Route("api/arter/get/tags")]
        public List<VArt> GetSpeciesByTags([FromQuery] string tagListAsJson)
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

            return speciesByTagsQuery.GetByTags(tagList ?? []);
        }
    }
}
