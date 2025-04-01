using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics.CodeAnalysis;

namespace BirdWare.Controllers
{
    [ApiController]
    public class TagController(ITagQuery tagQuery, IArtQueries artQueries, IMemoryCache memoryCache) : ControllerBase
    {
        private static bool enableCache = true;

        [NonAction]
        public void DisableCache()
        {
            enableCache = false;
        }

        [HttpGet]
        [Route("api/tags")]
        public List<Tag> GetTagList([FromQuery] string query)
        {
            var cacheEntry = GetOrCreate(tagQuery.GetTagList, memoryCache, "TagList");

            return [.. cacheEntry.Where(q => q.Name.IndexOf(query, StringComparison.CurrentCultureIgnoreCase) > -1)];
        }

        [HttpGet]
        [Route("api/tags/fugletur")]
        public List<Tag> GetTagListFugletur([FromQuery] string query)
        {
            var cacheEntry = GetOrCreate(tagQuery.GetTagListFugletur, memoryCache, "TagListFugletur");

            return [.. cacheEntry.Where(q => q.Name.IndexOf(query, StringComparison.CurrentCultureIgnoreCase) > -1)];
        }

        [Route("api/tag/{query}")]
        public Tag GetTag(string query)
        {
            var cacheEntry = GetOrCreate(tagQuery.GetTagList, memoryCache, "TagList");

            return cacheEntry
                .Where(t => t.Name.ToLower().Equals(query, StringComparison.CurrentCultureIgnoreCase))
                .FirstOrDefault() ?? new Tag();
        }

        [Route("api/tags/arter")]
        public List<Tag> GetTagsArter([FromQuery] string query)
        {
            var cacheEntry = GetOrCreate(tagQuery.GetTagList, memoryCache, "TagList");

            var list = cacheEntry
                .Where(t => t.TagType == TagTypes.Art &&
                       t.Name.IndexOf(query, StringComparison.CurrentCultureIgnoreCase) > -1).ToList() ?? [];

            return [.. list.OrderBy(o => o.Name)];
        }


        [Route("api/tag/art/{Id}")]
        public Tag GetArtTagById(long Id)
        {
            return artQueries.GetArtTagById(Id);
        }

        [ExcludeFromCodeCoverage(Justification = "IMemoryCache cannot be mocked, because it uses static extension methods")]
        private static List<Tag> GetOrCreate(Func<List<Tag>> getTagListMethod, IMemoryCache memoryCache, string cachedEntryName)
        {
            return enableCache ? memoryCache.GetOrCreate(cachedEntryName, entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromMinutes(10);
                return getTagListMethod();
            }) ?? [] : getTagListMethod();
        }
    }
}