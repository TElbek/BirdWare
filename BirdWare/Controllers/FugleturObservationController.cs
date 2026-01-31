using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BirdWare.Controllers
{
    [ApiController]
    public class FugleturObservationController(
        IFugleturQuery fugleturQuery,
        IFugleturObservationQuery fugleturObservationQuery,
        ISletObsCommand sletObsCommand) : ControllerBase
    {
        [HttpGet]
        [Route("api/fugletur/{id}/observationer")]
        public List<VObs> GetFugleturObservationer(long id)
        { 
            return fugleturObservationQuery.GetObservationer(id);
        }

        [HttpGet]
        [Route("api/fugletur/seneste/observationer")]
        public List<VObs> GetSenesteFugleturObservationer()
        {
            var fugleturId = fugleturQuery.GetSenesteFugletur();
            return fugleturObservationQuery.GetObservationer(fugleturId);
        }

        [HttpPost]
        [Route("api/fugletur/observation/{id}/slet")]
        public void SletObservation(long id)
        { 
            sletObsCommand.SletObservation(id);
        }
    }
}
