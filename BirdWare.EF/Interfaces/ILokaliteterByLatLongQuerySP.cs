using BirdWare.Domain.Models;

namespace BirdWare.EF.Interfaces
{
    public interface ILokaliteterByLatLongQuerySP
    {
        List<spLokaliteterByLatLongResult> FindLokaliteterLatLong(double latitude, double longitude);
    }
}
