using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using BirdWare.Validation;
using Microsoft.AspNetCore.Mvc;

namespace BirdWare.Controllers
{
    [ApiController]
    public class SynchController(ISynchTripCommand synchTripCommand, ISynchTripQuery synchTripQuery) : ControllerBase
    {
        [Route("api/synch/trip/{fugleturId:long}")]
        public SynchTrip GetTripAndObservations(long fugleturId)
        {
            var validator = new GreaterThanZeroValidator();
            if(!validator.Validate(fugleturId).IsValid)
            {
                throw new ArgumentException("fugleturId must be greater than zero.");
            }

            return synchTripQuery.GetTrip(fugleturId);
        }

        [Route("api/synch/trip/Post")]
        [HttpPost]
        public HttpResponseMessage AddTrip([FromBody] SynchTrip synchTrip)
        {
            return synchTripCommand.PostTrip(synchTrip) ?
                new HttpResponseMessage(System.Net.HttpStatusCode.OK) :
                new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }
    }
}