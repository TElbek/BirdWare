using BirdWare.Domain.Models;

namespace BirdWare.EF.Interfaces
{
    public interface IFugleturAnalyseQuery
    {
        List<FugleturAnalyse> Analyser(long fugleturId, AnalyseTyper analyseType);
    }
}
