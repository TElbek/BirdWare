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
            if (synchTrip.Fugletur.FugleturId != 0)
            {
                synchTripCommand.PostTrip(synchTrip);
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }

            return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }
    }
}
