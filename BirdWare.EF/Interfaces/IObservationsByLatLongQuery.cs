using BirdWare.Domain.Models;

namespace BirdWare.EF.Interfaces
{
    public interface IObservationsByLatLongQuery
    {
        List<ByLatitudeLongitude> GetObservationsByLatLong(List<Tag> tagList);
    }
}
