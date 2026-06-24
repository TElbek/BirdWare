using BirdWare.Domain.Models;

namespace BirdWare.EF.Interfaces
{
    public interface IBedsteMaanedForFamilieQuery
    {
        List<BedsteMaanedForFamilie> GetBedsteMaanedForFamilie();
    }
}
