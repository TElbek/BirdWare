using BirdWare.Domain.Models;
using System.Dynamic;

namespace BirdWare.Domain.GeoJsonHandlers
{
    public class ObservationToGeoJson
    {
        public static GeoJson MapObservationsToGeoJson(List<VObs> observationsByTags)
        {
            var geoJSON = new GeoJson();

            var ObservationsBylokalitet = from o in observationsByTags
                                          where (o.Latitude.HasValue && o.Longitude.HasValue && o.RegionId > 0)
                                          group o by new { o.LokalitetId, o.LokalitetNavn, o.Latitude, o.Longitude } into ogroup
                                          select new { key = ogroup.Key, count = ogroup.Count(), latestDate = ogroup.Max(m => m.Dato)};

            ObservationsBylokalitet.ToList().ForEach(lokalitet => {

                dynamic expandoObject = new ExpandoObject();
                expandoObject.id = lokalitet.key.LokalitetId;
                expandoObject.name = lokalitet.key.LokalitetNavn;
                expandoObject.count = lokalitet.count;
                expandoObject.countIsAboveAverage = lokalitet.count > ObservationsBylokalitet.Average(a => a.count);
                expandoObject.latestDate = lokalitet.latestDate ?? DateTime.MinValue;

                geoJSON.Features.Add(new GeoJsonFeature
                {
                    Properties = expandoObject,
                    Geometry = { Coordinates = [GetDoubleValueOrZero(lokalitet.key.Longitude), 
                                                GetDoubleValueOrZero(lokalitet.key.Latitude)]}
                });
            });

            return geoJSON;
        }

        private static double GetDoubleValueOrZero(double? value)
        { 
            return value ?? 0;
        }
    }
}
