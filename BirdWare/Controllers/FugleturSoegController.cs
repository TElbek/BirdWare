using BirdWare.Domain.Models;
using BirdWare.Domain.Utilities;
using BirdWare.EF.Interfaces;
using BirdWare.Validation;
using Microsoft.AspNetCore.Mvc;

namespace BirdWare.Controllers
{
    [ApiController]
    public class FugleturSoegController(
        IFugletureByTagsQuery fugletureByTagsQuery,
        IFugleturQuery fugleturQuery) : ControllerBase
    {
        [Route("api/fugleture/get/tags")]
        public List<VTur> GetFugletureByTags([FromQuery] string tagListAsJson)
        {
            var validator = new TagListAsJSONValidator();
            if (!validator.Validate(tagListAsJson).IsValid)
            {
                return [];
            }

            var tagList = JsonOperations<Tag>.GetListFromJSON(tagListAsJson);
            var tagListValidator = new TagListValidator();
            if(tagListValidator.Validate(tagList ?? []).IsValid == false)
            {
                return [];
            }

            return fugletureByTagsQuery.GetFugletureByTags(tagList ?? []);
        }     

        [Route("api/fugleture/aar/maaned")]
        public List<VTur> GetFugletureAarMaaned([FromQuery] long aarstal, long maaned)
        {
            var validator = new AarMaanedValidator();
            if(!validator.Validate((aarstal, maaned)).IsValid)
            {
                return [];
            }

            return fugleturQuery.GetFugleTureAarMaaned(aarstal,maaned);
        }
    }
}
