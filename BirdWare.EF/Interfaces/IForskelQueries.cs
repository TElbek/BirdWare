using BirdWare.Domain.Models;

namespace BirdWare.EF.Interfaces
{
    public interface IForskelQueries
    {
        IEnumerable<Forskel> GetForskelIAar();
        IEnumerable<Forskel> GetForskelSidsteAar();
    }
}
