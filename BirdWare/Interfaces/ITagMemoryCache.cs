using BirdWare.Domain.Models;

namespace BirdWare.Interfaces
{
    public interface ITagMemoryCache
    {
        List<Tag> GetOrCreate(Func<List<Tag>> getTagListMethod, string cachedEntryName);
    }
}
