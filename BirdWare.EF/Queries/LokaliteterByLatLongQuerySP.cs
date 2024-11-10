using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BirdWare.EF.Queries
{
    public class LokaliteterByLatLongQuerySP(BirdWareContext birdWareContext) : ILokaliteterByLatLongQuerySP
    {
        public List<spLokaliteterByLatLongResult> FindLokaliteterLatLong(double latitude, double longitude)
        {
            var sqlParameter1 = new SqlParameter("@Latitude", latitude);
            var sqlParameter2 = new SqlParameter("@Longitude", longitude);
            return [.. birdWareContext.SpLokaliteterByLatLongResult
                        .FromSqlRaw("spLokaliteterByLatLong @Latitude, @Longitude", [sqlParameter1, sqlParameter2])];
        }
    }
}
