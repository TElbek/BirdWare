using BirdWare.Domain.Entities;
using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using BirdWare.Validation;
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
        public IEnumerable<VObs> GetFugleturObservationer(long id)
        {
            var validator = new GreaterThanZeroValidator();
            if (!validator.Validate(id).IsValid)
            {
                return [];
            }
            return fugleturObservationQuery.GetObservationer(id);
        }

        [HttpGet]
        [Route("api/fugletur/seneste/observationer")]
        public IEnumerable<VObs> GetSenesteFugleturObservationer()
        {
            var fugleturId = fugleturQuery.GetSenesteFugletur();
            return fugleturObservationQuery.GetObservationer(fugleturId);
        }

        [HttpPost]
        [Route("api/fugletur/observation/{id}/slet")]
        public void SletObservation(long id)
        {
            var validator = new GreaterThanZeroValidator();
            if (!validator.Validate(id).IsValid)
            {
                return;
            }
            sletObsCommand.SletObservation(id);
        }
    }
}
