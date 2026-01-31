using BirdWare.Domain.Entities;
using BirdWare.Domain.Models;

namespace BirdWare.EF.Interfaces
{
    public interface IFugleturTagFilter
    {
        IQueryable<Fugletur> Filter(List<Tag> tagList, IQueryable<Fugletur> queryable);
    }
}
