using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BirdWare.Controllers
{
    [ApiController]
    public class FugleturObservationController(IFugleturObservationQuery fugleturObservationQuery) : ControllerBase
    {
        [HttpGet]
        [Route("api/fugletur/{id}/observationer")]
        public List<VObs> GetFugleturObservationer(long id)
        { 
            return fugleturObservationQuery.GetObservationer(id);
        }
    }
}
