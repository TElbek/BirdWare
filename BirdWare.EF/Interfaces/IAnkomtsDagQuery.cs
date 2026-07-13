using BirdWare.Domain.Models;

namespace BirdWare.EF.Interfaces
{
    public interface IAnkomtsDagQuery
    {
        ILookup<long, AnkomstDagBasis> GetAnkomstData(long familieId);
    }
}
