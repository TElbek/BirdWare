using BirdWare.Domain.Entities;
using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;

namespace BirdWare.EF.TagFilters
{
    public class FugleturSenesteAarFilter : IFugleturTagFilter
    {
        public IQueryable<Fugletur> Filter(List<Tag> tagList, IQueryable<Fugletur> queryable)
        {
            var senesteNaarTag = tagList
                                .Where(t => t.TagType == TagTypes.SenesteNÅr)
                                .OrderBy(t => t.SomeValue)
                                .FirstOrDefault() ?? new Tag { SomeValue = 0 };

            return queryable
                    .Where(q => q.Dato >= DateTime.Now.AddYears(-1 * (int)senesteNaarTag.SomeValue));
        }
    }
}
