using BirdWare.Domain.Models;

namespace BirdWare.EF.Interfaces
{
    public interface IAnkomtsDagQuery
    {
        Task<List<AnkomstDag>> GetAnkomtsDage(long familieId);
    }
}
