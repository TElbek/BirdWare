using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BirdWare.Controllers
{
    [ApiController]
    public class LokalitetController(ILokaliteterByLatLongQuerySP lokaliteterByLatLongQuerySP) : ControllerBase
    {
        [HttpGet]
        [Route("api/lokalitet/{latitude}/{longitude}")]
        public List<spLokaliteterByLatLongResult> FindLokaliteterUdfraLatLong(double latitude, double longitude)
        { 
            return lokaliteterByLatLongQuerySP.FindLokaliteterLatLong(latitude, longitude);
        }
    }
}
