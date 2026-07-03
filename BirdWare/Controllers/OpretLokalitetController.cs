using BirdWare.Domain.Entities;
using BirdWare.EF.Interfaces;
using BirdWare.Validation;
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
            var validator = new LokalitetValidator();

            if (!validator.Validate(lokalitet).IsValid)
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }

            return opretLokalitetCommand.OpretLokalitet(lokalitet) ?
                new HttpResponseMessage(System.Net.HttpStatusCode.OK) :
                new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }
    }
}
