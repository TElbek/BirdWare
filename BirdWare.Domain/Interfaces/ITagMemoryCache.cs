using BirdWare.Domain.Models;

namespace BirdWare.Domain.Interfaces
{
    public interface ITagMemoryCache
    {
        IEnumerable<Tag> GetOrCreate(Func<IEnumerable<Tag>> getTagListMethod, string cachedEntryName);
    }
}
