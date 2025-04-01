using BirdWare.Domain.Models;
using BirdWare.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics.CodeAnalysis;

namespace BirdWare.Cache
{
    [ExcludeFromCodeCoverage(Justification = "IMemoryCache cannot be mocked, because it uses static extension methods")]
    public class TagMemoryCache : ITagMemoryCache
    {
        public TagMemoryCache(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        public TagMemoryCache()
        {
            enableCache = false;
        }

        private readonly bool enableCache = true;
        private readonly IMemoryCache? memoryCache;

        public List<Tag> GetOrCreate(Func<List<Tag>> getTagListMethod, string cachedEntryName)
        {
            if (enableCache)
            {
                return memoryCache?.GetOrCreate(cachedEntryName, entry =>
                {
                    entry.SlidingExpiration = TimeSpan.FromMinutes(10);
                    return getTagListMethod();
                }) ?? [];
            }
            else
            { 
                return getTagListMethod();
            }
        }
    }
}
