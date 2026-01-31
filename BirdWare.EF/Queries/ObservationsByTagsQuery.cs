using BirdWare.Domain.Entities;
using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BirdWare.EF.Queries
{
    public class ObservationsByTagsQuery(BirdWareContext birdWareContext, IServiceProvider serviceProvider) : BaseByTagsQuery(birdWareContext), IObservationsByTagsQuery
    {
        public List<VObs> GetObservationsByTags(List<Tag> tagList)
        {
            IQueryable<Observation> observations = birdWareContext.Observation.AsNoTracking();

            foreach (var tagType in tagList.Select(r => r.TagType).Distinct())
            {
                var impl = serviceProvider.GetRequiredKeyedService<IObservationTagFilter>(tagType);
                var result = impl.Filter(tagList, observations);
                if (result is IQueryable<Observation> obs)
                {
                    observations = obs;
                }
            }
            return MapToResult(observations.OrderByDescending(r => r.FugleturId).Take(200));
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