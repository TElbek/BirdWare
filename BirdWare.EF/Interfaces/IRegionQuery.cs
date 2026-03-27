using BirdWare.Domain.Entities;

namespace BirdWare.EF.Interfaces
{
    public interface IRegionQuery
    {
        List<Region> GetList();
    }
}
