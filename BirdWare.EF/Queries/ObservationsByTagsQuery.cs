using BirdWare.Domain.Entities;
using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;

namespace BirdWare.EF.Queries
{
    public class ObservationsByTagsQuery(BirdWareContext birdWareContext) : BaseByTagsQuery(birdWareContext), IObservationsByTagsQuery
    {
        public List<VObs> GetObservationsByTags(List<Tag> TagList)
        {
            IQueryable<Observation> observations = birdWareContext.Observation;

            if (TagList.Any(t => t.TagType == TagTypes.Aarstal))
            {
                observations = FilterForYear(TagList, observations);
            }

            if (TagList.Any(t => t.TagType == TagTypes.Maaned))
            {
                observations = FilterForMonth(TagList, observations);
            }

            if (TagList.Any(q => ErSaesonTagType(q)))
            {
                observations = FilterForSeason(TagList, observations);
            }

            if (TagList.Any(t => t.TagType == TagTypes.Region))
            {
                observations = FilterForRegion(TagList, observations);
            }

            if (TagList.Any(t => t.TagType == TagTypes.Lokalitet))
            {
                observations = FilterForLocality(TagList, observations);
            }

            if (TagList.Any(t => t.TagType == TagTypes.Familie))
            {
                observations = FilterForFamily(TagList, observations);
            }

            if (TagList.Any(t => t.TagType == TagTypes.Gruppe))
            {
                observations = FilterForGroup(TagList, observations);
            }

            if (TagList.Any(t => t.TagType == TagTypes.Art))
            {
                observations = FilterForSpecies(TagList, observations);
            }

            return MapToResult(observations.OrderByDescending(r => r.FugleturId).Take(200));
        }

        private static IQueryable<Observation> FilterForSpecies(List<Tag> TagList, IQueryable<Observation> observations)
        {
            var list = TagList.Where(r => r.TagType == TagTypes.Art).Select(s => s.Id).Cast<long>().ToList();
            return observations.Where(g => list.Contains(g.ArtId));
        }

        private static IQueryable<Observation> FilterForGroup(List<Tag> TagList, IQueryable<Observation> observations)
        {
            var list = TagList.Where(r => r.TagType == TagTypes.Gruppe).Select(s => s.Id).Cast<long>().ToList();
            return observations.Where(g => list.Contains(g.Art.GruppeId));
        }

        private static IQueryable<Observation> FilterForFamily(List<Tag> TagList, IQueryable<Observation> observations)
        {
            var list = TagList.Where(r => r.TagType == TagTypes.Familie).Select(s => s.Id).Cast<long>().ToList();
            return observations.Where(g => list.Contains(g.Art.Gruppe.FamilieId));
        }

        private static IQueryable<Observation> FilterForLocality(List<Tag> TagList, IQueryable<Observation> observations)
        {
            var list = TagList.Where(r => r.TagType == TagTypes.Lokalitet).Select(s => s.Id).Cast<long>().ToList();
            return observations.Where(g => list.Contains(g.Fugletur.LokalitetId));
        }

        private static IQueryable<Observation> FilterForRegion(List<Tag> TagList, IQueryable<Observation> observations)
        {
            var list = TagList.Where(r => r.TagType == TagTypes.Region).Select(s => s.Id).Cast<long>().ToList();
            return observations.Where(g => list.Contains(g.Fugletur.Lokalitet.RegionId));
        }

        private IQueryable<Observation> FilterForSeason(List<Tag> TagList, IQueryable<Observation> observations)
        {
            var seasonTagTypes = TagList.Where(q => ErSaesonTagType(q)).Select(s => s.TagType).ToList();
            var months = SaesonMaaneder.Liste.Where(q => seasonTagTypes.Contains(q.Key)).SelectMany(s => s.Value).ToList();
            var withMaaned = birdWareContext.Fugletur.GetAarMaaned().Where(y => months.Contains(y.Maaned));
            return observations.Where(o => withMaaned.Any(a => a.FugleturId == o.FugleturId));
        }

        private IQueryable<Observation> FilterForMonth(List<Tag> TagList, IQueryable<Observation> observations)
        {
            var list = TagList.Where(r => r.TagType == TagTypes.Maaned).Select(s => s.Id).Cast<long>().ToList();
            var withMaaned = birdWareContext.Fugletur.GetAarMaaned().Where(y => list.Contains(y.Maaned));
            return observations.Where(o => withMaaned.Any(a => a.FugleturId == o.FugleturId));
        }

        private IQueryable<Observation> FilterForYear(List<Tag> TagList, IQueryable<Observation> observations)
        {
            var list = TagList.Where(r => r.TagType == TagTypes.Aarstal).Select(s => s.Id).Cast<long>().ToList();
            var withAarstal = birdWareContext.Fugletur.GetAarMaaned().Where(y => list.Contains(y.Aarstal));
            return observations.Where(o => withAarstal.Any(a => a.FugleturId == o.FugleturId));
        }

        private static List<VObs> MapToResult(IQueryable<Observation> observations)
        {
            return [.. observations.Select(m => new VObs()
            {
                Dato = m.Fugletur.Dato,
                ArtId = m.Art.Id,
                FamilieId = m.Art.Gruppe.FamilieId,
                FugleturId = m.Fugletur.Id,
                ArtNavn = m.Art.Navn ?? string.Empty,
                FamilieNavn = m.Art.Gruppe.Familie.Navn ?? string.Empty,
                GruppeNavn = m.Art.Gruppe.Navn ?? string.Empty,
                GruppeId = m.Art.GruppeId,
                SU = m.Art.SU,
                LokalitetNavn = m.Fugletur.Lokalitet.Navn ?? string.Empty,
                LokalitetId = m.Fugletur.Lokalitet.Id,
                Bem = m.Beskrivelse ?? string.Empty,
                RegionId = m.Fugletur.Lokalitet.RegionId,
                RegionNavn = m.Fugletur.Lokalitet.Region.Navn ?? string.Empty,
                Aarstal = m.Fugletur.Aarstal,
                Maaned = m.Fugletur.Maaned
            })];
        }
    }
}