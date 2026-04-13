using BirdWare.Domain.Models;

namespace BirdWare.EF.Interfaces
{
    public interface IAnkomtsDagSubQuery
    {
        Task<List<AnkomstDagBeregning>> GetAnkomstDagQuery(long familieId);
    }
}
