using BirdWare.Domain.Models;

namespace BirdWare.EF.Interfaces
{
    public interface IAnkomtsDagQuery
    {
        Task<IEnumerable<AnkomstDag>> GetAnkomtsDage(long familieId);
    }
}
