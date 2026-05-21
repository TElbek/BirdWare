using BirdWare.Domain.Models;

namespace BirdWare.EF.Interfaces
{
    public interface ILokaliteterByLatLongQuery
    {
        List<LokaliteterByLatLong> FindLokaliteterLatLong(double latitude, double longitude);
    }
}
