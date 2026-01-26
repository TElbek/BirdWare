using BirdWare.Domain.Entities;
using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BirdWare.EF.Queries
{
    public class ObservationsByTagsQuery(BirdWareContext birdWareContext) : BaseByTagsQuery(birdWareContext), IObservationsByTagsQuery
    {
        public List<VObs> GetObservationsByTags(List<Tag> tagList)
        {
            IQueryable<Observation> observations = birdWareContext.Observation.AsNoTracking();
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

        [TagFilter(TagType = TagTypes.Art)]
        private static IQueryable<Observation> FilterForSpecies(List<Tag> tagList, IQueryable<Observation> observations)
        {
            var tagIdsByTypeList = GetTagIdsByType(tagList, TagTypes.Art);
            return observations.Where(g => tagIdsByTypeList.Contains(g.ArtId));
        }

        [TagFilter(TagType = TagTypes.Gruppe)]
        private static IQueryable<Observation> FilterForGroup(List<Tag> tagList, IQueryable<Observation> observations)
        {
            var tagIdsByTypeList = GetTagIdsByType(tagList, TagTypes.Gruppe); ;
            return observations.Where(g => tagIdsByTypeList.Contains(g.Art.GruppeId));
        }

        [TagFilter(TagType = TagTypes.Familie)]
        private static IQueryable<Observation> FilterForFamily(List<Tag> tagList, IQueryable<Observation> observations)
        {
            var tagIdsByTypeList = GetTagIdsByType(tagList, TagTypes.Familie);
            return observations.Where(g => tagIdsByTypeList.Contains(g.Art.Gruppe.FamilieId));
        }

        [TagFilter(TagType = TagTypes.Lokalitet)]
        private static IQueryable<Observation> FilterForLocality(List<Tag> tagList, IQueryable<Observation> observations)
        {
            var tagIdsByTypeList = GetTagIdsByType(tagList, TagTypes.Lokalitet);
            return observations.Where(g => tagIdsByTypeList.Contains(g.Fugletur.LokalitetId));
        }

        [TagFilter(TagType = TagTypes.Region)]
        private static IQueryable<Observation> FilterForRegion(List<Tag> tagList, IQueryable<Observation> observations)
        {
            var tagIdsByTypeList = GetTagIdsByType(tagList, TagTypes.Region);
            return observations.Where(g => tagIdsByTypeList.Contains(g.Fugletur.Lokalitet.RegionId));
        }

        [TagFilter(TagType = TagTypes.Land)]
        private static IQueryable<Observation> FilterForCountry(List<Tag> tagList, IQueryable<Observation> observations)
        {
            return tagList.Any(q => q.Name == "Danmark") ? 
                observations.Where(g => g.Fugletur.Lokalitet.RegionId > 0) : 
                observations;
        }

        [TagFilter(TagTypeList = [TagTypes.SaesonEfteraar, TagTypes.SaesonForaar, TagTypes.SaesonSommer, TagTypes.SaesonVinter])]
        private IQueryable<Observation> FilterForSeason(List<Tag> tagList, IQueryable<Observation> observations)
        {
            var seasonTagTypes = tagList.Where(q => ErSaesonTagType(q)).Select(s => s.TagType).ToList();
            var months = SaesonMaaneder.Liste.Where(q => seasonTagTypes.Contains(q.Key)).SelectMany(s => s.Value).ToList();
            var withMaaned = birdWareContext.Fugletur.GetAarMaaned().Where(y => months.Contains(y.Maaned));
            return observations.Where(o => withMaaned.Any(a => a.FugleturId == o.FugleturId));
        }

        [TagFilter(TagType = TagTypes.Maaned)]
        private IQueryable<Observation> FilterForMonth(List<Tag> tagList, IQueryable<Observation> observations)
        {
            var tagIdsByTypeList = GetTagIdsByType(tagList, TagTypes.Maaned);
            var withMaaned = birdWareContext.Fugletur.GetAarMaaned().Where(y => tagIdsByTypeList.Contains(y.Maaned));
            return observations.Where(o => withMaaned.Any(a => a.FugleturId == o.FugleturId));
        }

        [TagFilter(TagType = TagTypes.Aarstal)]
        private IQueryable<Observation> FilterForYear(List<Tag> tagList, IQueryable<Observation> observations)
        {
            var tagIdsByTypeList = GetTagIdsByType(tagList, TagTypes.Aarstal);
            var withAarstal = birdWareContext.Fugletur.GetAarMaaned().Where(y => tagIdsByTypeList.Contains(y.Aarstal));
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