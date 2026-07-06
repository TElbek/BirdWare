using BirdWare.Domain.Entities;
using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;

namespace BirdWare.EF.Queries
{
    public class SpeciesByTagsQuery(
                    BirdWareContext birdWareContext, IServiceProvider serviceProvider) :
                    ObservationsByTagsQueryBase<VArt>(birdWareContext, serviceProvider), ISpeciesByTagsQuery
    {
        protected override List<VArt> GenerateResultSet(IQueryable<Observation> observations)
        {
            var resultSet = observations
                .Where(o => o.Fugletur.Lokalitet.RegionId > 0)
                .Join(birdWareContext.Art, o => o.ArtId, v => v.Id, (o, v) => v)                
                .Distinct();

            return [.. (from v in resultSet
                    select new VArt
                    {
                        ArtNavn = v.Navn ?? string.Empty,
                        ArtId = v.Id,
                        SU = v.SU,
                        Speciel = v.Speciel,
                        GruppeNavn = v.Gruppe.Navn ?? string.Empty,
                        GruppeId = v.GruppeId,
                        FamilieId = v.Gruppe.Familie.Id,
                        FamilieNavn = v.Gruppe.Familie.Navn ?? string.Empty
                    })];
        }
    }
}
