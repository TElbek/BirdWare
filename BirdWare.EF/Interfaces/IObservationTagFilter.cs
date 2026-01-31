using BirdWare.Domain.Entities;
using BirdWare.Domain.Models;

namespace BirdWare.EF.Interfaces
{
    public interface IObservationTagFilter
    {
        IQueryable<Observation> Filter(List<Tag> tagList, IQueryable<Observation> queryable);
    }
}
