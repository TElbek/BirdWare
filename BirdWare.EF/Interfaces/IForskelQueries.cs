using BirdWare.Domain.Models;

namespace BirdWare.EF.Interfaces
{
    public interface IForskelQueries
    {
        Task<List<Forskel>> GetForskelIAar();
        Task<List<Forskel>> GetForskelSidsteAar();
    }
}
