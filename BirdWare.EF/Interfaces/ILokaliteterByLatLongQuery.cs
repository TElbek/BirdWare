using BirdWare.Domain.Models;

namespace BirdWare.EF.Interfaces
{
    public interface ILokaliteterByLatLongQuery
    {
        IEnumerable<LokaliteterByLatLong> FindLokaliteterLatLong(double latitude, double longitude);
    }
}
