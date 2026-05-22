using BirdWare.Domain.Models;
using BirdWare.EF.Geography;
using BirdWare.EF.Interfaces;

namespace BirdWare.EF.Queries
{
    public class LokaliteterByLatLongQuery(BirdWareContext birdWareContext) : ILokaliteterByLatLongQuery
    {
        private const double metersPerKilometer = 1000;
        private const int twenty = 20;

        public List<LokaliteterByLatLong> FindLokaliteterLatLong(double latitude, double longitude)
        {
            var currentPoint = GeographyPoint.GetPointFromLatLong(latitude, longitude);

            var q = from l in birdWareContext.Lokalitet
                    where l.Point != null && l.Point.Distance(currentPoint) <= twenty * metersPerKilometer
                    orderby l.Point != null ? l.Point.Distance(currentPoint) : double.MaxValue
                    select new LokaliteterByLatLong
                    {
                        Id = l.Id,
                        Navn = l.Navn,
                        Distance = l.Point != null ? l.Point.Distance(currentPoint) / metersPerKilometer : 0
                    };

            return q.Any() ? [.. q] : [new() { Navn = "Ingen lokaliteter." }];
        }
    }
}
