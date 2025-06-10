using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;

namespace BirdWare.EF.Queries
{
    public class ObservationsByLatLongQuery(IObservationsByTagsQuery observationsByTagsQuery) : IObservationsByLatLongQuery
    {
        public List<ByLatitudeLongitude> GetObservationsByLatLong(List<Tag> tagList)
        { 
            var observationList = observationsByTagsQuery.GetObservationsByTags(tagList);
            return [.. (from o in observationList
                    group o by new { o.LokalitetId, o.LokalitetNavn, o.Latitude, o.Longitude } into g
                    select new ByLatitudeLongitude
                    {
                        Id = g.Key.LokalitetId,
                        Navn = g.Key.LokalitetNavn,
                        Latitude = g.Key.Latitude,
                        Longitude = g.Key.Longitude,
                        Count = g.Count()
                    })];
        }
    }
}
