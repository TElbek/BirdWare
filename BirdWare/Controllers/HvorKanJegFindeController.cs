using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BirdWare.Controllers
{
    [ApiController]
    public class HvorKanJegFindeController(IHvorKanJegFindeQuery hvorKanJegFindeQuery) : ControllerBase
    {
        [HttpGet]
        [Route("api/hvorkanjegfinde")]
        public List<spHvorKanJegFindeResult> HvorKanJegFinde()
        { 
            return hvorKanJegFindeQuery.GetHvorKanJegFinde();
        }
    }
}
