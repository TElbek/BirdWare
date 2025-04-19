using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BirdWare.Controllers
{
    [ApiController]
    public class OpretObsController(IOpretObsCommand opretObsCommand,
                                    IOpdaterObsCommand opdaterObsCommand) : ControllerBase
    {
        [Authorize]
        [Route("api/observation/opretobs/{artId}")]
        [HttpPost]
        public HttpResponseMessage OpretObs(long artId)
        {
            return (artId > 0 && opretObsCommand.OpretObsPåFugletur(artId)) ?
                new HttpResponseMessage(System.Net.HttpStatusCode.OK) : 
                new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }

        [Route("api/observation/opdater/")]
        [HttpPost]
        public HttpResponseMessage OpdaterObs([FromBody] VObs vObs)
        {
            return opdaterObsCommand.OpdaterObservation(vObs) ?
                new HttpResponseMessage(System.Net.HttpStatusCode.OK) :
                new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }
    }
}
