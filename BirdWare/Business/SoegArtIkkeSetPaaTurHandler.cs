using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using BirdWare.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BirdWare.Business
{
    public class SoegArtIkkeSetPaaTurHandler(ITagHandler tagHandler,
                                             IFugleturQuery fugleturQuery,
                                             IFugleturObservationQuery fugleturObservationQuery) : ISoegArtIkkeSetPaaTurHandler
    {
        public IEnumerable<Tag> GetTags([FromQuery] string query)
        {
            var tagList = tagHandler.GetTagListArt(query);

            var latestFugleturId = fugleturQuery.GetSenesteFugletur();
            var observedArtTagIds = fugleturObservationQuery.GetObservationer(latestFugleturId)
                .Select(t => t.ArtId);

            return tagList
                    .Where(q => !observedArtTagIds.Contains(q.Id))
                    .OrderBy(o => o.Name);
        }
    }
}
