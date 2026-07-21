using BirdWare.Domain.Models;
using BirdWare.Interfaces;
using BirdWare.Validation;
using Microsoft.AspNetCore.Mvc;

namespace BirdWare.Controllers
{

    [ApiController]
    public class FugleturAnalyseController(IFugleturAnalyseHandler fugleturAnalyseHandler) : ControllerBase
    {
        [HttpGet]
        [Route("api/fugletur/{fugleturId}/analyse/{analyseType}")]
        public IEnumerable<TripAnalysisResult> AnalyserFugletur(long fugleturId, AnalyseTyper analyseType) => 
               IsValid(fugleturId) ? fugleturAnalyseHandler.Analyser(fugleturId, analyseType) : [];

        [HttpGet]
        [Route("api/analyse/typer")]
        public IEnumerable<AnalyseTypeModel> GetAnalyseTypeListe() => 
            [.. Enum.GetValues<AnalyseTyper>().Select(s => new AnalyseTypeModel { AnalyseType = s })];

        private static bool IsValid(long fugleturId) => new GreaterThanZeroValidator().Validate(fugleturId).IsValid;
    }
}