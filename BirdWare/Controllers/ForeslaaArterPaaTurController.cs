using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BirdWare.Controllers
{
    [ApiController]
    public class ForeslaaArterPaaTurController(IForeslaaArterPaaTurQuery foreslaaArterPaaTurQuery) : ControllerBase
    {
        [HttpGet]
        [Route("api/fugletur/foreslaaArter")]
        public List<ArtForslag> ForeslaaArter()
        { 
            return foreslaaArterPaaTurQuery.ForeslaaArterSenesteTur();
        }
    }
}
