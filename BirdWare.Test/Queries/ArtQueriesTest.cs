using BirdWare.Domain.Entities;
using BirdWare.Domain.Models;
using BirdWare.EF.Queries;
using BirdWare.Test.Moq;

namespace BirdWare.Test.Queries
{
    public class ArtQueriesTest : DbContextMock
    {
        private readonly DbSetMock<Art> artMockSet = new();

        [Fact]
        public void GetArtTagByIdTest()
        {
            var artQueries = GetArtQueries();

            var tag = artQueries.GetArtTagById(1);
            Assert.Equal(1, tag.Id);
            Assert.Equal("Art1", tag.Name);
        }

        [Fact]
        public void GetArtTagByUnknownIdGetEmptyTagTest()
        {
            var artQueries = GetArtQueries();

            var idNotExist = artMockSet.DbSet.Max(q => q.Id) + 1;
            var tag = artQueries.GetArtTagById(idNotExist);
            Assert.Equal(0, tag.Id);
            Assert.Equal(string.Empty, tag.Name);
            Assert.Equal(TagTypes.Ukendt, tag.TagType);
        }


        private ArtQueries GetArtQueries()
        {
            artMockSet.AddData([
                new() { Id = 1, Navn = "Art1", GruppeId = 1 },
                new() { Id = 2, Navn = "Art2", GruppeId = 2 },
                new() { Id = 3, Navn = "Art3", GruppeId = 3 }]);

            MockContext.Setup(c => c.Art).Returns(artMockSet.DbSet);
            return new ArtQueries(MockContext.Object);
        }
    }
}