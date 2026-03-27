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
        public List<Region> GetList()
        {
            return regionQuery.GetList();
        }
    }
}
