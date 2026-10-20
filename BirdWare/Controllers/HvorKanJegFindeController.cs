using BirdWare.Domain.Models;
using BirdWare.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BirdWare.Controllers
{
    [ApiController]
    public class HvorKanJegFindeController(IHvorKanJegFindeHandler hvorKanJegFindeHandler) : ControllerBase
    {
        [HttpGet]
        [Route("api/hvorkanjegfinde")]
        public IEnumerable<HvorKanJegFindeResponse> HvorKanJegFinde()
        {
            return hvorKanJegFindeHandler.GetHvorKanJegFinde();
        }
    }
}
