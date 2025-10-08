using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace BirdWare.Controllers
{
    [ApiController]
    public class ForskelController(IForskelQueries forskelQueries) : ControllerBase
    {
        [HttpGet]
        [OutputCache]
        [Route("api/forskel/iaar")]
        public List<Forskel> ForskelIAar()
        { 
            return forskelQueries.GetForskelIAar();
        }

        [HttpGet]
        [OutputCache]
        [Route("api/forskel/sidsteaar")]
        public List<Forskel> ForskelSidsteAar()
        {
            return forskelQueries.GetForskelSidsteAar();
        }

    }
}
