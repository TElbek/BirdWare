using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;

namespace BirdWare.EF.Queries
{
    public class ArtQueries(BirdWareContext birdWareContext) : ContextBase(birdWareContext), IArtQueries
    {
        public Tag GetArtTagById(long Id)
        {
            var art = birdWareContext.Art.FirstOrDefault(a => a.Id == Id);
            return art != null ? new Tag { 
                Id = art.Id, 
                Name = art.Navn ?? string.Empty, 
                TagType = TagTypes.Art, 
                ParentId = art.GruppeId } : new Tag();
        }
    }
}
