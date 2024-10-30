using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BirdWare.Controllers
{

    [ApiController]
    public class FugleturAnalyseController(IFugleturAnalyseQuerySP fugleturAnalyseQuerySP) : ControllerBase
    {
        [HttpGet]
        [Route("api/fugletur/{fugleturId}/analyse/")]
        public List<SpTripAnalysisResult> AnalyserFugletur(long fugleturId)
        { 
            return fugleturAnalyseQuerySP.Analyser(fugleturId);
        }

        [HttpGet]
        [Route("api/analyse/typer")]
        public List<AnalyseTypeModel> GetAnalyseTypeListe()
        { 
            return Enum.GetValues<AnalyseTyper>()
                        .Select(s => new AnalyseTypeModel {AnalyseType = s})
                        .ToList();
        }
    }
}
