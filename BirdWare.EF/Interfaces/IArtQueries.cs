using BirdWare.Domain.Models;

namespace BirdWare.EF.Interfaces
{
    public interface IArtQueries
    {
        Tag GetArtTagById(long Id);
    }
}
