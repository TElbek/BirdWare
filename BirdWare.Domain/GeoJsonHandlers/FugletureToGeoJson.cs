using BirdWare.Domain.Models;
using System.Dynamic;

namespace BirdWare.Domain.GeoJsonHandlers
{
    public class FugletureToGeoJson
    {
        public static GeoJson MapFugletureToGeoJson(List<VTur> fugleture)
        {
            var geoJSON = new GeoJson();

            var FugletureByLokalitet = from f in fugleture
                               where (f.Latitude.HasValue && f.Longitude.HasValue && f.RegionId > 0)
                               group f by new { f.LokalitetId, f.LokalitetNavn, f.Latitude, f.Longitude } into ogroup
                               select new { key = ogroup.Key, count = ogroup.Count() };

            FugletureByLokalitet.ToList().ForEach(tur => {

                dynamic expandoObject = new ExpandoObject();
                expandoObject.Id = tur.key.LokalitetId;
                expandoObject.Name = tur.key.LokalitetNavn;
                expandoObject.Count = tur.count;
                expandoObject.CountIsAboveAverage = tur.count > FugletureByLokalitet.Average(a => a.count);

                geoJSON.Features.Add(
                    new GeoJsonFeature
                    {
                        Properties = expandoObject,
                        Geometry = { Coordinates = [
                                        tur.key.Longitude != null ? tur.key.Longitude.Value : 0,
                                        tur.key.Latitude  != null ? tur.key.Latitude.Value : 0] }
                    });
                }
            );

            return geoJSON;
        }
    }
}
