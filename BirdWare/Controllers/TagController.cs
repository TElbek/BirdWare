using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using BirdWare.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BirdWare.Controllers
{
    [ApiController]
    public class TagController(ITagQuery tagQuery, 
                               IArtQueries artQueries, 
                               IFugleturQuery fugleturQuery,
                               IFugleturObservationQuery fugleturObservationQuery,
                               ITagMemoryCache tagMemoryCache) : ControllerBase
    {
        [HttpGet]
        [Route("api/tags")]
        public List<TagGroup> GetTagList([FromQuery] string query)
        {
            var cacheEntry = tagMemoryCache.GetOrCreate(tagQuery.GetTagList, "TagList");
            var result = cacheEntry.Where(q => q.Name.IndexOf(query, StringComparison.CurrentCultureIgnoreCase) > -1);
            return GroupTagsByTypeName(result);
        }

        [HttpGet]
        [Route("api/tags/fugletur")]
        public List<TagGroup> GetTagListFugletur([FromQuery] string query)
        {
            var cacheEntry = tagMemoryCache.GetOrCreate(tagQuery.GetTagListFugletur, "TagListFugletur");

            var result = cacheEntry.Where(q => q.Name.IndexOf(query, StringComparison.CurrentCultureIgnoreCase) > -1);
            return GroupTagsByTypeName(result);
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

            var latestFugleturId = fugleturQuery.GetSenesteFugletur();
            var observedArtTagIds = fugleturObservationQuery.GetObservationer(latestFugleturId)
                .Select(t => t.ArtId);

            return [.. list
                    .Where(q => !observedArtTagIds.Contains(q.Id))
                    .OrderBy(o => o.Name)];
        }


        [Route("api/tag/art/{Id}")]
        public Tag GetArtTagById(long Id)
        {
            return artQueries.GetArtTagById(Id);
        }

        [Route("api/tags/familie")]
        public List<Tag> GetFamilieTagsBySearchValue([FromQuery] string query)
        {
            var cacheEntry = tagMemoryCache.GetOrCreate(tagQuery.GetTagList, "TagList");
            return [.. cacheEntry.Where(q => q.TagType == TagTypes.Familie &&
                                            q.Name.IndexOf(query, StringComparison.CurrentCultureIgnoreCase) > -1)];
        }

        private static List<TagGroup> GroupTagsByTypeName(IEnumerable<Tag> result)
        {
            return [.. result
                        .GroupBy(q => q.TypeName)
                        .Select(s => new TagGroup { Name = s.Key, Tags = [.. s.OrderBy(o => o.Name)] })
                        .OrderBy(o => o.Name)];
        }
    }
}