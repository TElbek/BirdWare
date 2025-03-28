using BirdWare.Domain.Entities;
using BirdWare.EF.Queries;

namespace BirdWare.Test
{
    public class ArtQueriesTest : QueriesTestBase<Art>
    {
        [Fact]
        public void GetArtTagByIdTest()
        {
            var artQueries = Arrange();

            var tag = artQueries.GetArtTagById(1);
            Assert.Equal(1, tag.Id);
            Assert.Equal("Art1", tag.Name);
        }

        [Fact]
        public void GetArtTagByIdNotFoundReturnEmptyTagTest()
        {
            var artQueries = Arrange();

            var tag = artQueries.GetArtTagById(13);
            Assert.Equal(0, tag.Id);
            Assert.Equal(string.Empty, tag.Name);
        }


        private ArtQueries Arrange()
        {
            AddData([
                new() { Id = 1, Navn = "Art1", GruppeId = 1 },
                new() { Id = 2, Navn = "Art2", GruppeId = 2 },
                new() { Id = 3, Navn = "Art3", GruppeId = 3 }]);

            MockContext.Setup(c => c.Art).Returns(MockSet.Object);
            return new ArtQueries(MockContext.Object);
        }
    }
}