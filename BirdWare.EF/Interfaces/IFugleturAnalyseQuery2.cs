using BirdWare.Domain.Models;

namespace BirdWare.EF.Interfaces
{
    internal interface IFugleturAnalyseQuery2
    {
        public List<FugleturAnalyse> Analyser(long fugleturId);
    }
}
