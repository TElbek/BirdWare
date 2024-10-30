using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace BirdWare.Controllers
{
    [ApiController]
    public class TagController(ITagQuery tagQuery, IArtQueries artQueries, IMemoryCache memoryCache) : ControllerBase
    {
        [HttpGet]
        [Route("api/tags")]
        public List<Tag> GetTagList([FromQuery] string query)
        {
            var cacheEntry = GetCachedEntry(tagQuery, memoryCache);

            return cacheEntry?
                .Where(q => q.Name.IndexOf(query, StringComparison.CurrentCultureIgnoreCase) > -1)
                .ToList() ?? [];
        }

        [HttpGet]
        [Route("api/tags/fugletur")]
        public List<Tag> GetTagListFugletur([FromQuery] string query)
        {
            var cacheEntry = GetCachedEntryFugletur(tagQuery, memoryCache);

            return cacheEntry?
                .Where(q => q.Name.IndexOf(query, StringComparison.CurrentCultureIgnoreCase) > -1)
                .ToList() ?? [];
        }

        [Route("api/tag/{query}")]
        public Tag GetTag(string query)
        {
            var cacheEntry = GetCachedEntry(tagQuery, memoryCache);

            return cacheEntry?
                .Where(t => t.Name.ToLower().Equals(query, StringComparison.CurrentCultureIgnoreCase))
                .FirstOrDefault() ?? new Tag();
        }

        [Route("api/tag/art/{Id}")]
        public Tag GetArtTagById(long Id)
        {
            return artQueries.GetArtTagById(Id);
        }

        private static List<Tag>? GetCachedEntry(ITagQuery tagQuery, IMemoryCache memoryCache)
        {
            return memoryCache.GetOrCreate("TagList", entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromMinutes(10);
                return tagQuery.GetTagList();
            });
        }

        private static List<Tag>? GetCachedEntryFugletur(ITagQuery tagQuery, IMemoryCache memoryCache)
        {
            return memoryCache.GetOrCreate("TagListFugletur", entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromMinutes(10);
                return tagQuery.GetTagListFugletur();
            });
        }
    }
}