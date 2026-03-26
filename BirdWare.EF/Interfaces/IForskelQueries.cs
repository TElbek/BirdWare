using BirdWare.Domain.Models;

namespace BirdWare.EF.Interfaces
{
    public interface IForskelQueries
    {
        List<Forskel> GetForskelIAar();
        List<Forskel> GetForskelSidsteAar();
    }
}
