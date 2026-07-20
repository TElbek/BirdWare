using BirdWare.Domain.Entities;
using BirdWare.Domain.Models;

namespace BirdWare.EF.Interfaces
{
    public interface IFugleturAnalyseQuery
    {
        List<Art> HentArtListe(long fugleturId);
        VTur FindFugletur(long fugleturId);
        ILookup<long, FugleturAnalyseData> FindAnalyseData(long fugleturId);
    }
}
