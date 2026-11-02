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
        public static async Task<List<VObs>> GetOrderStatus(
                ITagHandler tagHandler,
                IObservationsByTagsQuery observationsByTagsQuery,
                [Description("Artnavn, e.g. Solsort")] string artNavn)
        {
            var artTag = tagHandler.GetTag(artNavn);
            return [.. observationsByTagsQuery.GetByTags([artTag])];
        }
    }
}