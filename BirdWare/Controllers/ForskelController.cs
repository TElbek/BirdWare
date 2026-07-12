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
        public IEnumerable<Forskel> ForskelIAar()
        { 
            return forskelQueries.GetForskelIAar();
        }

        [HttpGet]
        [Route("api/forskel/sidsteaar")]
        public IEnumerable<Forskel> ForskelSidsteAar()
        {
            return forskelQueries.GetForskelSidsteAar();
        }

    }
}
