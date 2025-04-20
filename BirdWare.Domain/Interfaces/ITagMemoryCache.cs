using BirdWare.Domain.Models;

namespace BirdWare.Domain.Interfaces
{
    public interface ITagMemoryCache
    {
        List<Tag> GetOrCreate(Func<List<Tag>> getTagListMethod, string cachedEntryName);
    }
}
