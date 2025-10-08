using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace BirdWare.Controllers
{
    [ApiController]
    public class HvorKanJegFindeController(IHvorKanJegFindeQuery hvorKanJegFindeQuery) : ControllerBase
    {
        [HttpGet]
        [OutputCache]
        [Route("api/hvorkanjegfinde")]
        public List<spHvorKanJegFindeResult> HvorKanJegFinde()
        { 
            return hvorKanJegFindeQuery.GetHvorKanJegFinde();
        }
    }
}
