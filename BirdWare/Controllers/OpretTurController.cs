using BirdWare.EF.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BirdWare.Controllers
{
    [ApiController]
    public class OpretTurController(IOpretTurCommand opretTurCommand) : ControllerBase
    {
        [Route("api/fugletur/oprettur/{lokalitetId}")]
        [HttpPost]
        public HttpResponseMessage OpretTur(long lokalitetId)
        {
            if (lokalitetId > 0)
            {
                if (opretTurCommand.OpretTurPaaLokalitet(lokalitetId))
                {
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
                }
            }
            return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }
    }
}
