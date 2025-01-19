using BirdWare.EF.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BirdWare.Controllers
{
    [ApiController]
    public class OpretObsController(IOpretObsCommand opretObsCommand) : ControllerBase
    {
        [Route("api/observation/opretobs/{artId}")]
        [HttpPost]
        public HttpResponseMessage OpretObs(long artId)
        {
            if (artId > 0)
            {
                if (opretObsCommand.OpretObsPåFugletur(artId))
                {
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
                }
            }
            return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }
    }
}
