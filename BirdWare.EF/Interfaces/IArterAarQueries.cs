using BirdWare.Domain.Models;

namespace BirdWare.EF.Interfaces
{
    public interface IArterAarQueries
    {
        IQueryable<ArterAar> GetArterIAar();
        IQueryable<ArterAar> GetArterSidsteAar();
    }
}
