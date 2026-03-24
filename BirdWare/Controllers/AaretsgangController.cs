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
        public async Task<List<AaretsGang>> GetAaretsGang()
        { 
            return await aaretsGangQuery.GetAaretsGang();
        }
    }
}
