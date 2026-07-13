using BirdWare.Domain.Models;

namespace BirdWare.EF.Interfaces
{
    public interface IAnkomtsDagSubQuery
    {
        Task<IEnumerable<AnkomstDagBeregning>> GetAnkomstDagQuery(long familieId);
    }
}
