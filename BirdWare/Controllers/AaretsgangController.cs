using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BirdWare.Controllers
{
    [ApiController]
    public class AaretsgangController(IAaretsGangQuery aaretsGangQuery) : ControllerBase
    {
        [HttpGet]
        [Route("api/aaretsgang")]
        public List<AaretsGang> GetAaretsGang()
        { 
            return aaretsGangQuery.GetAaretsGang();
        }
    }
}
