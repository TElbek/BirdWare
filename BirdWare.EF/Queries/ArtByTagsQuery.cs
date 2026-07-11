using BirdWare.Domain.Entities;
using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BirdWare.EF.Queries
{
    public class ArtByTagsQuery(
                    BirdWareContext birdWareContext, IServiceProvider serviceProvider) :
                    ObservationsByTagsQueryBase<VArt>(birdWareContext, serviceProvider), IArtByTagsQuery
    {
        protected override List<VArt> GenerateResultSet(IQueryable<Observation> observations)
        {
            var resultSet = (from o in observations
                             where o.Fugletur.Lokalitet.RegionId > 0
                             join a in birdWareContext.Art.AsNoTracking() on o.ArtId equals a.Id
                             select a).Distinct();

            return [.. from v in resultSet select new VArt {
                       ArtNavn = v.Navn ?? string.Empty,
                       ArtId = v.Id,
                       SU = v.SU,
                       Speciel = v.Speciel,
                       GruppeNavn = v.Gruppe.Navn ?? string.Empty,
                       GruppeId = v.GruppeId,
                       FamilieId = v.Gruppe.Familie.Id,
                       FamilieNavn = v.Gruppe.Familie.Navn ?? string.Empty
                    }];
        }
    }
}