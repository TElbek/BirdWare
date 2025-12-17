using BirdWare.Domain.GeoJsonHandlers;
using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

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
            var tagList = JsonSerializer.Deserialize<List<Tag>>(tagListAsJson);
            return fugletureByTagsQuery.GetFugletureByTags(tagList ?? []);
        }

        [Route("api/fugleture/get/tags/geojson")]
        public GeoJson GetFugletureByTagsAsGeoJson([FromQuery] string tagListAsJson)
        {
            var tagList = JsonSerializer.Deserialize<List<Tag>>(tagListAsJson);
            var fugletureList = fugletureByTagsQuery.GetFugletureByTags(tagList ?? []);
            return FugletureToGeoJson.MapFugletureToGeoJson(fugletureList);
        }


        [Route("api/fugleture/aar/maaned")]
        public List<VTur> GetFugletureAarMaaned([FromQuery] long aarstal, long maaned)
        {
            return fugleturQuery.GetFugleTureAarMaaned(aarstal,maaned);
        }
    }
}
