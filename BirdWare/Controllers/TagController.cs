using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using BirdWare.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BirdWare.Controllers
{
    [ApiController]
    public class TagController(ITagQuery tagQuery, 
                               IArtQueries artQueries, 
                               ITagMemoryCache tagMemoryCache) : ControllerBase
    {
        [HttpGet]
        [Route("api/tags")]
        public List<Tag> GetTagList([FromQuery] string query)
        {
            var cacheEntry = tagMemoryCache.GetOrCreate(tagQuery.GetTagList, "TagList");

            return [.. cacheEntry.Where(q => q.Name.IndexOf(query, StringComparison.CurrentCultureIgnoreCase) > -1)];
        }

        [HttpGet]
        [Route("api/tags/fugletur")]
        public List<Tag> GetTagListFugletur([FromQuery] string query)
        {
            var cacheEntry = tagMemoryCache.GetOrCreate(tagQuery.GetTagListFugletur, "TagListFugletur");

            return [.. cacheEntry.Where(q => q.Name.IndexOf(query, StringComparison.CurrentCultureIgnoreCase) > -1)];
        }

        [Route("api/tag/{query}")]
        public Tag GetTag(string query)
        {
            var cacheEntry = tagMemoryCache.GetOrCreate(tagQuery.GetTagList, "TagList");

            return cacheEntry
                .Where(t => t.Name.ToLower().Equals(query, StringComparison.CurrentCultureIgnoreCase))
                .FirstOrDefault() ?? new Tag();
        }

        [Route("api/tags/arter")]
        public List<Tag> GetTagsArter([FromQuery] string query)
        {
            var cacheEntry = tagMemoryCache.GetOrCreate(tagQuery.GetTagList, "TagList");

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
    }
}