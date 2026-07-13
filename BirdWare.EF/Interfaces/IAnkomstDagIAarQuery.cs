using BirdWare.Domain.Models;

namespace BirdWare.EF.Interfaces
{
    public interface IAnkomstDagIAarQuery
    {
        Task<IEnumerable<AnkomstDagBeregning>> GetIAarQuery(long familieId);
    }
}
