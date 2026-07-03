using BirdWare.Domain.Models;
using BirdWare.Domain.Utilities;
using BirdWare.EF.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BirdWare.Controllers
{
    [ApiController]
    public class ObservationLatitudeLongitudeController(IObservationsByLatLongQuery observationsByLatLongQuery) : ControllerBase
    {
        [Route("api/observationer/get/latitude/longitude")]
        public List<ByLatitudeLongitude> GetObservationsByTags([FromQuery] string tagListAsJson)
        {
            var tagList = JsonOperations<Tag>.GetListFromJSON(tagListAsJson);
            return observationsByLatLongQuery.GetObservationsByLatLong(tagList ?? []);
        }
    }
}
