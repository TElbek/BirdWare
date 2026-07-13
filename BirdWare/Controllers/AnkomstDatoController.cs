using BirdWare.Business;
using BirdWare.Domain.Models;
using BirdWare.Validation;
using Microsoft.AspNetCore.Mvc;

namespace BirdWare.Controllers
{
    [ApiController]
    public class AnkomstDatoController(IAnkomtsDagHandler ankomtsDagHandler) : ControllerBase
    {
        [HttpGet]
        [Route("api/ankomstdato/familie/{familieId}")]
        public IEnumerable<AnkomstDag> AnkomstDagFamilie(long familieId)
        {
            return GetIsValid(familieId) ? ankomtsDagHandler.Handle(familieId) : [];
        }

        private static bool GetIsValid(long familieId) => 
                new GreaterThanZeroValidator().Validate(familieId).IsValid;
    }
}