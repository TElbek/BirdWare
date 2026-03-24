using BirdWare.Domain.Models;

namespace BirdWare.EF.Interfaces
{
    public interface IArterAarQueries
    {
        Task<IQueryable<ArterAar>> GetArterIAar();
        Task<IQueryable<ArterAar>> GetArterSidsteAar();
    }
}
