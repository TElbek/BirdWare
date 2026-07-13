using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using BirdWare.Validation;
using Microsoft.AspNetCore.Mvc;

namespace BirdWare.Controllers
{
    [ApiController]
    public class AnkomstDatoController(IAnkomtsDagQuery ankomtsDagQuery) : ControllerBase
    {
        [HttpGet]
        [Route("api/ankomstdato/familie/{familieId}")]
        public async Task<IEnumerable<AnkomstDag>> AnkomstDagFamilie(long familieId)
        {
            var validator = new GreaterThanZeroValidator();
            var validationResult = await validator.ValidateAsync(familieId);
            if (!validationResult.IsValid) return [];

            return await ankomtsDagQuery.GetAnkomtsDage(familieId);
        }
    }
}
