using BirdWare.Domain.Entities;
using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using System.Reflection;

namespace BirdWare.EF.Queries
{
    public class ObservationsByTagsQuery(BirdWareContext birdWareContext) : BaseByTagsQuery(birdWareContext), IObservationsByTagsQuery
    {
        public List<VObs> GetObservationsByTags(List<Tag> tagList)
        {
            IQueryable<Observation> observations = birdWareContext.Observation;
            var filterMethods = HentFilterMetoder();

            foreach (var tagType in tagList.Select(r => r.TagType).Distinct())
            {
                var method = FindFilterMetodeForTagType(tagType, filterMethods);
                var result = method?.Invoke(this, [tagList, observations]);
                if (result is IQueryable<Observation> obs)
                {
                    observations = obs;
                }
            }
            return MapToResult(observations.OrderByDescending(r => r.FugleturId).Take(200));
        }

        private static MethodInfo? FindFilterMetodeForTagType(TagTypes tagType, MethodInfo[] methodInfoList)
        {
            var allMethodsWithAttribute = methodInfoList
                            .Select(b => new {attr = b.GetCustomAttribute<ObservationTagFilterAttribute>(), method = b });

            var methodAttribute = allMethodsWithAttribute
               .FirstOrDefault(q => q.attr != null && (q.attr.TagType == tagType || q.attr.TagTypeList.Contains(tagType)));

            return methodAttribute?.method;
        }

        private MethodInfo[] HentFilterMetoder()
        {
            return GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
        }

        [ObservationTagFilter(TagType = TagTypes.Art)]
        private static IQueryable<Observation> FilterForSpecies(List<Tag> TagList, IQueryable<Observation> observations)
        {
            var list = TagList.Where(r => r.TagType == TagTypes.Art).Select(s => s.Id).Cast<long>().ToList();
            return observations.Where(g => list.Contains(g.ArtId));
        }

        [ObservationTagFilter(TagType = TagTypes.Gruppe)]
        private static IQueryable<Observation> FilterForGroup(List<Tag> tagList, IQueryable<Observation> observations)
        {
            var list = tagList.Where(r => r.TagType == TagTypes.Gruppe).Select(s => s.Id).Cast<long>().ToList();
            return observations.Where(g => list.Contains(g.Art.GruppeId));
        }

        [ObservationTagFilter(TagType = TagTypes.Familie)]
        private static IQueryable<Observation> FilterForFamily(List<Tag> tagList, IQueryable<Observation> observations)
        {
            var list = tagList.Where(r => r.TagType == TagTypes.Familie).Select(s => s.Id).Cast<long>().ToList();
            return observations.Where(g => list.Contains(g.Art.Gruppe.FamilieId));
        }

        [ObservationTagFilter(TagType = TagTypes.Lokalitet)]
        private static IQueryable<Observation> FilterForLocality(List<Tag> tagList, IQueryable<Observation> observations)
        {
            var list = tagList.Where(r => r.TagType == TagTypes.Lokalitet).Select(s => s.Id).Cast<long>().ToList();
            return observations.Where(g => list.Contains(g.Fugletur.LokalitetId));
        }

        [ObservationTagFilter(TagType = TagTypes.Region)]
        private static IQueryable<Observation> FilterForRegion(List<Tag> tagList, IQueryable<Observation> observations)
        {
            var list = tagList.Where(r => r.TagType == TagTypes.Region).Select(s => s.Id).Cast<long>().ToList();
            return observations.Where(g => list.Contains(g.Fugletur.Lokalitet.RegionId));
        }

        [ObservationTagFilter(TagType = TagTypes.Land)]
        private static IQueryable<Observation> FilterForCountry(List<Tag> tagList, IQueryable<Observation> observations)
        {
            return tagList.Any(q => q.Name == "Danmark") ? 
                observations.Where(g => g.Fugletur.Lokalitet.RegionId > 0) : 
                observations;
        }

        [ObservationTagFilter(TagTypeList = [TagTypes.SaesonEfteraar, TagTypes.SaesonForaar, TagTypes.SaesonSommer, TagTypes.SaesonVinter])]
        private IQueryable<Observation> FilterForSeason(List<Tag> tagList, IQueryable<Observation> observations)
        {
            var seasonTagTypes = tagList.Where(q => ErSaesonTagType(q)).Select(s => s.TagType).ToList();
            var months = SaesonMaaneder.Liste.Where(q => seasonTagTypes.Contains(q.Key)).SelectMany(s => s.Value).ToList();
            var withMaaned = birdWareContext.Fugletur.GetAarMaaned().Where(y => months.Contains(y.Maaned));
            return observations.Where(o => withMaaned.Any(a => a.FugleturId == o.FugleturId));
        }

        [ObservationTagFilter(TagType = TagTypes.Maaned)]
        private IQueryable<Observation> FilterForMonth(List<Tag> tagList, IQueryable<Observation> observations)
        {
            var list = tagList.Where(r => r.TagType == TagTypes.Maaned).Select(s => s.Id).Cast<long>().ToList();
            var withMaaned = birdWareContext.Fugletur.GetAarMaaned().Where(y => list.Contains(y.Maaned));
            return observations.Where(o => withMaaned.Any(a => a.FugleturId == o.FugleturId));
        }

        [ObservationTagFilter(TagType = TagTypes.Aarstal)]
        private IQueryable<Observation> FilterForYear(List<Tag> tagList, IQueryable<Observation> observations)
        {
            var list = tagList.Where(r => r.TagType == TagTypes.Aarstal).Select(s => s.Id).Cast<long>().ToList();
            var withAarstal = birdWareContext.Fugletur.GetAarMaaned().Where(y => list.Contains(y.Aarstal));
            return observations.Where(o => withAarstal.Any(a => a.FugleturId == o.FugleturId));
        }

        private static List<VObs> MapToResult(IQueryable<Observation> observations)
        {
            return [.. observations.Select(m => new VObs()
            {
                ObservationId = m.Id,
                Dato = m.Fugletur.Dato,
                ArtId = m.Art.Id,
                FamilieId = m.Art.Gruppe.FamilieId,
                FugleturId = m.Fugletur.Id,
                ArtNavn = m.Art.Navn ?? string.Empty,
                FamilieNavn = m.Art.Gruppe.Familie.Navn ?? string.Empty,
                GruppeNavn = m.Art.Gruppe.Navn ?? string.Empty,
                GruppeId = m.Art.GruppeId,
                SU = m.Art.SU,
                Speciel = m.Art.Speciel,
                LokalitetNavn = m.Fugletur.Lokalitet.Navn ?? string.Empty,
                LokalitetId = m.Fugletur.Lokalitet.Id,
                Latitude = m.Fugletur.Lokalitet.Latitude,
                Longitude = m.Fugletur.Lokalitet.Longitude,
                Bem = m.Beskrivelse ?? string.Empty,
                RegionId = m.Fugletur.Lokalitet.RegionId,
                RegionNavn = m.Fugletur.Lokalitet.Region.Navn ?? string.Empty,
                Aarstal = m.Fugletur.Aarstal,
                Maaned = m.Fugletur.Maaned
            })];
        }
    }
}