using BirdWare.Domain.Models;

namespace BirdWare.EF.Interfaces
{
    public interface IFugleturQuery
    {
        VTur GetFugletur(long id);
    }
}
