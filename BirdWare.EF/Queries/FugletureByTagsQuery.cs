using BirdWare.Domain.Entities;
using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BirdWare.EF.Queries
{
    public class FugletureByTagsQuery(BirdWareContext birdWareContext) : BaseByTagsQuery(birdWareContext), IFugletureByTagsQuery
    {        
        public List<VTur> GetFugletureByTags(List<Tag> tagList)
        {
            IQueryable<Fugletur> fugleture = birdWareContext.Fugletur.AsNoTracking();

            var filterMethods = HentFilterMetoder();

            foreach (var tagType in tagList.Select(r => r.TagType).Distinct())
            {
                var method = FindFilterMetodeForTagType(tagType, filterMethods);
                var result = method?.Invoke(this, [tagList, fugleture]);
                if (result is IQueryable<Fugletur> fugletur)
                {
                    fugleture = fugletur;
                }
            }

            return MapToResult(fugleture.OrderByDescending(r => r.Id).Take(50));
        }

        [TagFilter(TagType = TagTypes.Lokalitet)]
        private static IQueryable<Fugletur> FilterForLocality(List<Tag> tagList, IQueryable<Fugletur> fugleture)
        {
            var list = GetTagIdsByType(tagList, TagTypes.Lokalitet);
            return fugleture.Where(g => list.Contains(g.LokalitetId));
        }

        [TagFilter(TagType = TagTypes.Region)]
        private static IQueryable<Fugletur> FilterForRegion(List<Tag> tagList, IQueryable<Fugletur> fugleture)
        {
            var list = GetTagIdsByType(tagList, TagTypes.Region);
            return fugleture.Where(g => list.Contains(g.Lokalitet.RegionId));
        }

        [TagFilter(TagTypeList = [TagTypes.SaesonEfteraar, TagTypes.SaesonForaar, TagTypes.SaesonSommer, TagTypes.SaesonVinter])]
        private IQueryable<Fugletur> FilterForSeason(List<Tag> tagList, IQueryable<Fugletur> fugleture)
        {
            var seasonTagTypes = tagList.Where(q => ErSaesonTagType(q)).Select(s => s.TagType).ToList();
            var months = SaesonMaaneder.Liste.Where(q => seasonTagTypes.Contains(q.Key)).SelectMany(s => s.Value).ToList();
            var withMaaned = birdWareContext.Fugletur.GetAarMaaned().Where(y => months.Contains(y.Maaned));
            return fugleture.Where(f => withMaaned.Any(a => a.FugleturId == f.Id));
        }

        [TagFilter(TagType = TagTypes.Maaned)]
        private IQueryable<Fugletur> FilterForMonth(List<Tag> tagList, IQueryable<Fugletur> fugleture)
        {
            var list = GetTagIdsByType(tagList, TagTypes.Maaned);
            var withAarstal = birdWareContext.Fugletur.GetAarMaaned().Where(y => list.Contains(y.Maaned));
            return fugleture.Where(f => withAarstal.Any(a => a.FugleturId == f.Id));
        }

        [TagFilter(TagType = TagTypes.Aarstal)]
        private IQueryable<Fugletur> FilterForYear(List<Tag> tagList, IQueryable<Fugletur> fugleture)
        {
            var list = GetTagIdsByType(tagList, TagTypes.Aarstal);
            var withAarstal = birdWareContext.Fugletur.GetAarMaaned().Where(y => list.Contains(y.Aarstal));
            return fugleture.Where(f => withAarstal.Any(a => a.FugleturId == f.Id));
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