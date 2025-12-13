using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BirdWare.Controllers
{
    [ApiController]
    public class ObservationSoegController(IObservationsByTagsQuery observationsByTagsQuery) : ControllerBase
    {
        [Route("api/observationer/get/tags")]
        public List<VObs> GetObservationsByTags([FromQuery] string tagListAsJson)
        {
            var tagList = JsonSerializer.Deserialize<List<Tag>>(tagListAsJson);
            return observationsByTagsQuery.GetObservationsByTags(tagList ?? []);
        }

        [Route("api/observationer/get/tags/geojson")]
        public GeoJson GetObservationsByTagsAsGeoJSON([FromQuery] string tagListAsJson)
        {
            var tagList = JsonSerializer.Deserialize<List<Tag>>(tagListAsJson);
            var observationsByTags = observationsByTagsQuery.GetObservationsByTags(tagList ?? []);
            var geoJSON = new GeoJson();

            var ObservationsBylokalitet = from o in observationsByTags
                                          where (o.Latitude.HasValue && o.Longitude.HasValue && o.RegionId > 0)
                                          group o by new { o.LokalitetId, o.LokalitetNavn, o.Latitude, o.Longitude } into ogroup
                                          select new { key = ogroup.Key, count = ogroup.Count() };

            ObservationsBylokalitet.ToList().ForEach(lokalitet =>
                geoJSON.Features.Add(
                    new GeoJsonFeature
                    {
                        Properties = { ID = lokalitet.key.LokalitetId, Name = lokalitet.key.LokalitetNavn, Count = lokalitet.count },
                        Geometry = { Coordinates = [
                            lokalitet.key.Longitude != null ? lokalitet.key.Longitude.Value : 0,
                            lokalitet.key.Latitude  != null ? lokalitet.key.Latitude.Value : 0] }
                    })
            );

            geoJSON.Features
                .ForEach(f => f.Properties.CountIsAboveAverage =
                              f.Properties.Count > geoJSON.Features.Average(a => a.Properties.Count));

            return geoJSON;
        }
    }
}
