using BirdWare.Domain.Entities;
using BirdWare.Domain.Models;

namespace BirdWare.EF.TagFilters
{
    public class ObservationSenesteAarFilter : ObservationTagFilter
    {
        public override IQueryable<Observation> Filter(List<Tag> tagList, IQueryable<Observation> queryable)
        {           
            var senesteNaarTag = tagList
                                    .Where(t => t.TagType == TagTypes.SenesteNÅr)
                                    .OrderBy(t => t.SomeValue)
                                    .FirstOrDefault() ?? new Tag { SomeValue = 0 };

            return queryable
                    .Where(q => q.Fugletur.Dato >= DateTime.Now.AddYears(-1 * (int)senesteNaarTag.SomeValue));
        }
    }
}
