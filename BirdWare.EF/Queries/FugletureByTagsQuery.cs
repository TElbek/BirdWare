using BirdWare.Domain.Entities;
using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BirdWare.EF.Queries
{
    public class FugletureByTagsQuery(
                    BirdWareContext birdWareContext, IServiceProvider serviceProvider) : 
                    BaseByTagsQuery(birdWareContext, serviceProvider), IFugletureByTagsQuery
    {        
        public List<VTur> GetFugletureByTags(List<Tag> tagList)
        {
            IQueryable<Fugletur> fugleture = birdWareContext.Fugletur.AsNoTracking();

            tagList.Select(r => r.TagType).Distinct().ToList().ForEach(tagType =>
            {
                var fugleturTagFilter = GetFilterForTagType<IFugleturTagFilter>(tagType);
                var result = fugleturTagFilter.Filter(tagList, fugleture);
                if (result is IQueryable<Fugletur> fugleturResult)
                {
                    fugleture = fugleturResult;
                }
            });

            return MapToResult(fugleture.OrderByDescending(r => r.Id).Take(50));
        }

        private static List<VTur> MapToResult(IQueryable<Fugletur> fugleture)
        {
            return [.. fugleture.Select(f => new VTur()
            {
                Dato = f.Dato,
                Id = f.Id,
                LokalitetNavn = f.Lokalitet.Navn ?? string.Empty,
                LokalitetId = f.Lokalitet.Id,
                RegionId = f.Lokalitet.RegionId,
                Latitude = f.Lokalitet.Latitude,
                Longitude = f.Lokalitet.Longitude,
                RegionNavn = f.Lokalitet.Region.Navn ?? string.Empty,
                Aarstal = f.Aarstal,
                Maaned = f.Maaned
            })];

        }
    }
}