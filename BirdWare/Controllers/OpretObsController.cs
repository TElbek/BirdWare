using BirdWare.Domain.Entities;
using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using BirdWare.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BirdWare.Controllers
{
    [ApiController]
    public class OpretObsController(IOpretObsCommand opretObsCommand,
                                    IOpdaterObsCommand opdaterObsCommand) : ControllerBase
    {
        [Authorize]
        [Route("api/observation/opretobs/")]
        [HttpPost]
        public HttpResponseMessage OpretObs([FromBody] Observation observation)
        {
            var validator = new GreaterThanZeroValidator();
            if (!validator.Validate(observation.ArtId).IsValid)
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }

            return (observation.ArtId > 0 && opretObsCommand.OpretObsPåFugletur(observation.ArtId)) ?
                new HttpResponseMessage(System.Net.HttpStatusCode.OK) : 
                new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }

        [Route("api/observation/opdater/")]
        [HttpPost]
        public HttpResponseMessage OpdaterObs([FromBody] VObs vObs)
        {
            var validator = new OpdaterObsValidator();
            if (!validator.Validate(vObs).IsValid)
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }

            return opdaterObsCommand.OpdaterObservation(vObs) ?
                new HttpResponseMessage(System.Net.HttpStatusCode.OK) :
                new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }
    }
}
