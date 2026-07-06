using BirdWare.Domain.Entities;
using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BirdWare.EF.Queries
{
    public class ObservationsByTagsQuery(
                    BirdWareContext birdWareContext, IServiceProvider serviceProvider) :
                    ObservationsByTagsQueryBase<VObs>(birdWareContext, serviceProvider), IObservationsByTagsQuery
    {
        protected override List<VObs> GenerateResultSet(IQueryable<Observation> observations)
        {
            var query = from obs in observations
                join tur in birdWareContext.Fugletur.AsNoTracking() on obs.FugleturId equals tur.Id
                join lok in birdWareContext.Lokalitet.AsNoTracking() on tur.LokalitetId equals lok.Id
                join kgrp in birdWareContext.Kommune.AsNoTracking() on lok.KommuneId equals kgrp.Id into kgroup
                from kom in kgroup.DefaultIfEmpty()
                join reg in birdWareContext.Region.AsNoTracking() on lok.RegionId equals reg.Id
                join art in birdWareContext.Art.AsNoTracking() on obs.ArtId equals art.Id
                join grp in birdWareContext.Gruppe.AsNoTracking() on art.GruppeId equals grp.Id
                join fam in birdWareContext.Familie.AsNoTracking() on grp.FamilieId equals fam.Id
                orderby tur.Dato descending
                select new { obs, tur, lok, kom, reg, art, grp, fam };

                return [.. query.Take(200).AsEnumerable()
                                .Select(x => VObs.MapToVObs(x.obs, x.tur, x.lok, x.kom, x.reg, x.art, x.grp, x.fam))];
        }        
    }
}