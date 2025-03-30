using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BirdWare.Controllers
{
    [ApiController]
    public class SynchController(ISynchTripCommand synchTripCommand, ISynchTripQuery synchTripQuery) : ControllerBase
    {
        [Route("api/synch/trip/{fugleturId:long}")]
        public SynchTrip GetTripAndObservations(long fugleturId)
        {
            return synchTripQuery.GetTrip(fugleturId);
        }

        [Route("api/synch/trip/Post")]
        [HttpPost]
        public HttpResponseMessage AddTrip([FromBody] SynchTrip synchTrip)
        {
            return synchTrip.Fugletur.FugleturId > 0 && synchTripCommand.PostTrip(synchTrip) ?
                new HttpResponseMessage(System.Net.HttpStatusCode.OK) :
                new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }
    }
}