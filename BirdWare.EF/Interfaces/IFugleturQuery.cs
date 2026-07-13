using BirdWare.Domain.Models;

namespace BirdWare.EF.Interfaces
{
    public interface IFugleturQuery
    {
        VTur GetFugletur(long id);
        IEnumerable<VTur> GetFugleTureAarMaaned(long aarstal, long maaned);
        long GetSenesteFugletur();
    }
}
