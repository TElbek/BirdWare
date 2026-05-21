using BirdWare.Domain.Entities;
using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BirdWare.Controllers
{
    [ApiController]
    public class LokalitetController(ILokaliteterByLatLongQuery lokaliteterByLatLongQuery) : ControllerBase
    {
        [HttpGet]
        [Route("api/lokalitet/{latitude}/{longitude}")]
        public List<LokaliteterByLatLong> FindLokaliteterLatLong(double latitude, double longitude)
        { 
            return lokaliteterByLatLongQuery.FindLokaliteterLatLong(latitude, longitude);
        }

        [HttpGet]
        [Route("api/lokalitet/ny")]
        public Lokalitet CreateLokalitet()
        { 
            return new Lokalitet();
        }
    }
}
