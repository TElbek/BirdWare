using BirdWare.Domain.Models;

namespace BirdWare.EF.Interfaces
{
    public interface IAnkomtsDagQuery
    {
        List<AnkomstDag> GetAnkomtsDage(long familieId);
    }
}
