using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BirdWare.Controllers
{
    [ApiController]
    public class FugleturController(IFugleturQuery fugleturQuery) : ControllerBase
    {
        [HttpGet]
        [Route("api/fugletur/{id}")]
        public VTur GetFugletur(long id)
        {
            return fugleturQuery.GetFugletur(id);
        }
    }
}
