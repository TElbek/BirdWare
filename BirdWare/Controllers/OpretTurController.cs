using BirdWare.EF.Interfaces;
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
            return lokalitetId > 0 && opretTurCommand.OpretTurPaaLokalitet(lokalitetId) ?
                new HttpResponseMessage(HttpStatusCode.OK) :
                new HttpResponseMessage(HttpStatusCode.BadRequest);
        }
    }
}
