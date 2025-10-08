using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace BirdWare.Controllers
{
    [ApiController]
    public class FugleturController(IFugleturQuery fugleturQuery) : ControllerBase
    {
        [HttpGet]
        [OutputCache]
        [Route("api/fugletur/{id}")]
        public VTur GetFugletur(long id)
        {
            return fugleturQuery.GetFugletur(id);
        }

        [HttpGet]
        [Route("api/fugletur/seneste/id")]
        public long GetSenesteFugleturId()
        {
            return fugleturQuery.GetSenesteFugletur();
        }
    }
}
