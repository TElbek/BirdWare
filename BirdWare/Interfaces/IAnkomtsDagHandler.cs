using BirdWare.Domain.Models;

namespace BirdWare.Business
{
    public interface IAnkomtsDagHandler
    {
        IEnumerable<AnkomstDag> Handle(long familieId);
    }
}
