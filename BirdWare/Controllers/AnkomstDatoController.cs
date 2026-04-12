using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BirdWare.Controllers
{
    [ApiController]
    public class AnkomstDatoController(IAnkomtsDagQuery ankomtsDagQuery) : ControllerBase
    {
        [HttpGet]
        [Route("api/ankomstdato/familie/{familieId}")]
        public List<AnkomstDag> AnkomstDagFamilie(long familieId)
        {
            return ankomtsDagQuery.GetAnkomtsDage(familieId);
        }
    }
}
