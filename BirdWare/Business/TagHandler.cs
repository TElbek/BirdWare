using BirdWare.Domain.Interfaces;
using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using BirdWare.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BirdWare.Business
{
    internal class TagHandler(ITagQuery tagQuery, ITagMemoryCache tagMemoryCache) : ITagHandler
    {
        public List<TagGroup> GetTagList(string query)
        {
            var cacheEntry = tagMemoryCache.GetOrCreate(tagQuery.GetTagList, "TagList");
            var result = cacheEntry.Where(q => q.Name.IndexOf(query, StringComparison.CurrentCultureIgnoreCase) > -1);
            return GroupTagsByTypeName(result);
        }

        public List<TagGroup> GetTagListFugletur(string query)
        {
            var cacheEntry = tagMemoryCache.GetOrCreate(tagQuery.GetTagListFugletur, "TagListFugletur");
            var result = cacheEntry.Where(q => q.Name.IndexOf(query, StringComparison.CurrentCultureIgnoreCase) > -1);
            return GroupTagsByTypeName(result);
        }

        public List<Tag> GetTagListArt(string query)
        {
            var cacheEntry = tagMemoryCache.GetOrCreate(tagQuery.GetTagList, "TagList");
            return [.. cacheEntry
                .Where(t => t.TagType == TagTypes.Art &&
                       t.Name.IndexOf(query, StringComparison.CurrentCultureIgnoreCase) > -1)
                .OrderBy(t => t.Name)];
        }

        public Tag GetTag(string query)
        {
            var cacheEntry = tagMemoryCache.GetOrCreate(tagQuery.GetTagList, "TagList");

            return cacheEntry
                .Where(t => t.Name.ToLower().Equals(query, StringComparison.CurrentCultureIgnoreCase))
                .FirstOrDefault() ?? new Tag();
        }

        public List<Tag> GetFamilieTagsBySearchValue([FromQuery] string query)
        {
            var cacheEntry = tagMemoryCache.GetOrCreate(tagQuery.GetTagList, "TagList");
            return [.. cacheEntry.Where(q => q.TagType == TagTypes.Familie &&
                                             q.Name.IndexOf(query, StringComparison.CurrentCultureIgnoreCase) > -1)];
        }

        public string GetTagsFromNamesAsJSON([FromQuery] string tagNamesAsJson)
        {
            var tagNames = JsonSerializer.Deserialize<string[]>(tagNamesAsJson);

            var cacheEntry = tagMemoryCache.GetOrCreate(tagQuery.GetTagList, "TagList");
            var tagList = cacheEntry.Where(q => tagNames.Contains(q.Name)).ToList();
            return JsonSerializer.Serialize(tagList);
        }

        private static List<TagGroup> GroupTagsByTypeName(IEnumerable<Tag> tags)
        {
            return [.. tags
                .GroupBy(t => t.TagType)
                .Select(g => new TagGroup
                {
                    Name = g.Key.ToString(),
                    Tags = [.. g.OrderBy(t => t.Name)]
                })
                .OrderBy(g => g.Name)];
        }
    }
}
