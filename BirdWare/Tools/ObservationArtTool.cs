using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using BirdWare.Interfaces;
using ModelContextProtocol.Server;
using System.ComponentModel;

namespace BirdWare.Tools
{
    [McpServerToolType]
    public class ObservationArtTool
    {
        [McpServerTool]
        [Description("Hent observationer baseret på art.")]
        public static List<VObs> GetObservationerAfArt(
                ITagHandler tagHandler,
                IObservationsByTagsQuery observationsByTagsQuery,
                [Description("Artnavn, e.g. Solsort")] string artNavn)
        {
            var artTag = tagHandler.GetTag(artNavn);
            var aarstaltag = tagHandler.GetTag("Seneste 5 år");

            return artTag.TagType == TagTypes.Art && aarstaltag.TagType == TagTypes.SenesteNÅr ?
                   [.. observationsByTagsQuery.GetByTags([artTag, aarstaltag])] : [];
        }
    }
}