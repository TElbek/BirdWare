using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BirdWare.Controllers
{
    [ApiController]
    public class ObservationLatitudeLongitudeController(IObservationsByLatLongQuery observationsByLatLongQuery) : ControllerBase
    {
        [Route("api/observationer/get/latitude/longitude")]
        public List<ByLatitudeLongitude> GetObservationsByTags([FromQuery] string tagListAsJson)
        {
            var tagList = JsonSerializer.Deserialize<List<Tag>>(tagListAsJson);
            return observationsByLatLongQuery.GetObservationsByLatLong(tagList ?? []);
        }
    }
}
