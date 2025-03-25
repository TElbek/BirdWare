using BirdWare.Domain.Entities;
using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;

namespace BirdWare.EF.Queries
{
    public class FugletureByTagsQuery(BirdWareContext birdWareContext) : BaseByTagsQuery(birdWareContext), IFugletureByTagsQuery
    {        
        public List<VTur> GetFugletureByTags(List<Tag> TagList)
        {
            IQueryable<Fugletur> fugleture = birdWareContext.Fugletur;

            if (TagList.Any(t => t.TagType == TagTypes.Aarstal))
            {
                fugleture = FilterForYear(TagList, fugleture);
            }

            if (TagList.Any(t => t.TagType == TagTypes.Maaned))
            {
                fugleture = FilterForMonth(TagList, fugleture);
            }

            if (TagList.Any(q => ErSaesonTagType(q)))
            {
                fugleture = FilterForSeason(TagList, fugleture);
            }

            if (TagList.Any(t => t.TagType == TagTypes.Region))
            {
                fugleture = FilterForRegion(TagList, fugleture);
            }

            if (TagList.Any(t => t.TagType == TagTypes.Lokalitet))
            {
                fugleture = FilterForLocality(TagList, fugleture);
            }

            return MapToResult(fugleture.OrderByDescending(r => r.Id).Take(50));
        }

        private static IQueryable<Fugletur> FilterForLocality(List<Tag> TagList, IQueryable<Fugletur> fugleture)
        {
            var list = TagList.Where(r => r.TagType == TagTypes.Lokalitet).Select(s => s.Id).Cast<long>().ToList();
            return fugleture.Where(g => list.Contains(g.LokalitetId));
        }

        private static IQueryable<Fugletur> FilterForRegion(List<Tag> TagList, IQueryable<Fugletur> fugleture)
        {
            var list = TagList.Where(r => r.TagType == TagTypes.Region).Select(s => s.Id).Cast<long>().ToList();
            return fugleture.Where(g => list.Contains(g.Lokalitet.RegionId));
        }

        private IQueryable<Fugletur> FilterForSeason(List<Tag> TagList, IQueryable<Fugletur> fugleture)
        {
            var seasonTagTypes = TagList.Where(q => ErSaesonTagType(q)).Select(s => s.TagType).ToList();
            var months = SaesonMaaneder.Liste.Where(q => seasonTagTypes.Contains(q.Key)).SelectMany(s => s.Value).ToList();
            var withMaaned = birdWareContext.Fugletur.GetAarMaaned().Where(y => months.Contains(y.Maaned));
            return fugleture.Where(f => withMaaned.Any(a => a.FugleturId == f.Id));
        }

        private IQueryable<Fugletur> FilterForMonth(List<Tag> TagList, IQueryable<Fugletur> fugleture)
        {
            var list = TagList.Where(r => r.TagType == TagTypes.Maaned).Select(s => s.Id).Cast<long>().ToList();
            var withAarstal = birdWareContext.Fugletur.GetAarMaaned().Where(y => list.Contains(y.Maaned));
            return fugleture.Where(f => withAarstal.Any(a => a.FugleturId == f.Id));
        }

        private IQueryable<Fugletur> FilterForYear(List<Tag> TagList, IQueryable<Fugletur> fugleture)
        {
            var list = TagList.Where(r => r.TagType == TagTypes.Aarstal).Select(s => s.Id).Cast<long>().ToList();
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
                RegionNavn = f.Lokalitet.Region.Navn ?? string.Empty,
                Aarstal = f.Aarstal,
                Maaned = f.Maaned
            })];

        }
    }
}
