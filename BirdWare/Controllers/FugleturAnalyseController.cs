using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BirdWare.Controllers
{

    [ApiController]
    public class FugleturAnalyseController(IFugleturAnalyseQuery fugleturAnalyseQuery) : ControllerBase
    {
        [HttpGet]
        [Route("api/fugletur/{fugleturId}/analyse/{analyseType}")]
        public List<TripAnalysisResult> AnalyserFugletur(long fugleturId, AnalyseTyper analyseType)
        { 
            return fugleturAnalyseQuery.Analyser(fugleturId, analyseType);
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
