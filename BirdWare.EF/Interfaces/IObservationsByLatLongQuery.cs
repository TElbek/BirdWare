using BirdWare.Domain.Models;

namespace BirdWare.EF.Interfaces
{
    public interface IObservationsByLatLongQuery
    {
        IEnumerable<ByLatitudeLongitude> GetObservationsByLatLong(List<Tag> tagList);
    }
}
