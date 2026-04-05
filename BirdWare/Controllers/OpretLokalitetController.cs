using BirdWare.Domain.Entities;
using BirdWare.EF.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BirdWare.Controllers
{
    public class OpretLokalitetController(IOpretLokalitetCommand opretLokalitetCommand) : ControllerBase
    {
        [Authorize]
        [Route("api/lokalitet/opret")]
        [HttpPost]
        public HttpResponseMessage OpretLokalitet([FromBody] Lokalitet lokalitet)
        { 
            return opretLokalitetCommand.OpretLokalitet(lokalitet) ?
                new HttpResponseMessage(System.Net.HttpStatusCode.OK) :
                new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }
    }
}
