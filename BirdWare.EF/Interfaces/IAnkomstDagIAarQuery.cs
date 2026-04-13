using BirdWare.Domain.Models;

namespace BirdWare.EF.Interfaces
{
    public interface IAnkomstDagIAarQuery
    {
        Task<List<AnkomstDagBeregning>> GetIAarQuery(long familieId);
    }
}
