using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BirdWare.Controllers
{
    [ApiController]
    public class ForskelController(IForskelQueries forskelQueries) : ControllerBase
    {
        [HttpGet]
        [Route("api/forskel/iaar")]
        public async Task<List<Forskel>> ForskelIAar()
        { 
            return await forskelQueries.GetForskelIAar();
        }

        [HttpGet]
        [Route("api/forskel/sidsteaar")]
        public async Task<List<Forskel>> ForskelSidsteAar()
        {
            return await forskelQueries.GetForskelSidsteAar();
        }

    }
}
