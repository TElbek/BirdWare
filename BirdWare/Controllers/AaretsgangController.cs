using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace BirdWare.Controllers
{
    [ApiController]
    public class AaretsgangController(IAaretsGangQuery aaretsGangQuery) : ControllerBase
    {
        [HttpGet]
        [OutputCache]
        [Route("api/aaretsgang")]
        public List<AaretsGang> GetAaretsGang()
        { 
            return aaretsGangQuery.GetAaretsGang();
        }
    }
}
