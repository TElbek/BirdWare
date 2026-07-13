using BirdWare.Domain.Entities;
using BirdWare.EF.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BirdWare.Controllers
{
    [ApiController]
    public class RegionController(IRegionQuery regionQuery) : ControllerBase
    {
        [HttpGet]
        [Route("api/regioner")]
        public IEnumerable<Region> GetList()
        {
            return regionQuery.GetList();
        }

        [HttpGet]
        [Route("api/regioner/inkludland")]
        public IEnumerable<Region> GetListinkludland()
        {
            return regionQuery.GetList(inklUdland: true);
        }
        
    }
}
