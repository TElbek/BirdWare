using BirdWare.EF.Interfaces;
using BirdWare.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BirdWare.Controllers
{
    [ApiController]
    [Authorize]
    public class OpretTurController(IOpretTurCommand opretTurCommand) : ControllerBase
    {
        [Route("api/fugletur/oprettur/{lokalitetId}")]
        [HttpPost]
        public HttpResponseMessage OpretTur(long lokalitetId)
        {
            var validator = new GreaterThanZeroValidator();
            if(!validator.Validate(lokalitetId).IsValid)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            return opretTurCommand.OpretTurPaaLokalitet(lokalitetId) ?
                new HttpResponseMessage(HttpStatusCode.OK) :
                new HttpResponseMessage(HttpStatusCode.BadRequest);
        }
    }
}
