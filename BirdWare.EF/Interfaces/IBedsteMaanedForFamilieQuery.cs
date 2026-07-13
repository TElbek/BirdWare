using BirdWare.Domain.Models;

namespace BirdWare.EF.Interfaces
{
    public interface IBedsteMaanedForFamilieQuery
    {
        IEnumerable<BedsteMaanedForFamilie> GetBedsteMaanedForFamilie();
    }
}
