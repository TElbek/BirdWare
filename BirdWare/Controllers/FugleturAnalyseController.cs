using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using BirdWare.Validation;
using Microsoft.AspNetCore.Mvc;

namespace BirdWare.Controllers
{

    [ApiController]
    public class FugleturAnalyseController(IFugleturAnalyseQuery fugleturAnalyseQuery) : ControllerBase
    {
        [HttpGet]
        [Route("api/fugletur/{fugleturId}/analyse")]
        public List<TripAnalysisResult> AnalyserFugletur(long fugleturId)
        {
            var validator = new GreaterThanZeroValidator();
            if(!validator.Validate(fugleturId).IsValid)
            {
                return [];
            }

            return fugleturAnalyseQuery.Analyser(fugleturId);
        }

        [HttpGet]
        [Route("api/analyse/typer")]
        public List<AnalyseTypeModel> GetAnalyseTypeListe()
        { 
            return [.. Enum.GetValues<AnalyseTyper>().Select(s => new AnalyseTypeModel {AnalyseType = s})];
        }
    }
}
