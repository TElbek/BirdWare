using BirdWare.Domain.Entities;

namespace BirdWare.EF.Interfaces
{
    public interface IRegionQuery
    {
        IEnumerable<Region> GetList(bool inklUdland = false);
    }
}
