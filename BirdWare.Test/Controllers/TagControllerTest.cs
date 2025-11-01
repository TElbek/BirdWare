using BirdWare.Domain.Cache;
using BirdWare.Controllers;
using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using Moq;

namespace BirdWare.Test.Controllers
{
    public class TagControllerTest
    {
        private readonly Mock<ITagQuery> tagQueryQueryMock = new();
        private readonly Mock<IArtQueries> artQueriesMock = new();
        private readonly Mock<TagMemoryCache> tagMemoryCacheMock = new();
        private readonly Mock<IFugleturObservationQuery> fugleturObservationQueryMock = new();
        private readonly Mock<IFugleturQuery> fugleturQueryMock = new();

        private readonly TagController tagController;

        public TagControllerTest()
        {
            tagQueryQueryMock.Setup(x => x.GetTagList()).Returns([
                new Tag {Id = 1, Name = "Musvåge", TagType = TagTypes.Art},
                new Tag {Id = 2, Name = "Sjælland", TagType = TagTypes.Region},
            ]);

            tagQueryQueryMock.Setup(x => x.GetTagListFugletur()).Returns([
                new Tag {Id = 2, Name = "Vestamager", TagType = TagTypes.Lokalitet}
            ]);

            artQueriesMock.Setup(x => x.GetArtTagById(11)).Returns(new Tag {Id = 11, Name = "Drosselrørsanger", TagType = TagTypes.Art});
            artQueriesMock.Setup(x => x.GetArtTagById(10)).Returns(new Tag ());

            tagController = new TagController(
                                        tagQueryQueryMock.Object, 
                                        artQueriesMock.Object, 
                                        fugleturQueryMock.Object,
                                        fugleturObservationQueryMock.Object,
                                        tagMemoryCacheMock.Object);
        }

        [Fact]
        public void GetTagListTest()
        {
            tagController.GetTagList("query");
            tagQueryQueryMock.Verify(x => x.GetTagList(), Times.Once);
        }

        [Fact]
        public void GetTagListTestTagFound()
        {
            var list = tagController.GetTagList("Musvåge");
            Assert.Single(list);
        }

        [Fact]
        public void GetTagListTestTagNotFound()
        {
            var list = tagController.GetTagList("Sjagger");
            Assert.Empty(list);
        }

        [Fact]
        public void GetTagListFugleturTest()
        {
            tagController.GetTagListFugletur("query");
            tagQueryQueryMock.Verify(x => x.GetTagListFugletur(), Times.Once);
        }

        [Fact]
        public void GetTagListFugleturTagFoundTest()
        {
            var list = tagController.GetTagListFugletur("Vestamager");
            Assert.Single(list);
        }

        [Fact]
        public void GetTagListFugleturTagNotFoundTest()
        {
            var list = tagController.GetTagListFugletur("Skagen");
            Assert.Empty(list);
        }

        [Fact]
        public void GetTagTest()
        {
            tagController.GetTag("query");
            tagQueryQueryMock.Verify(x => x.GetTagList(), Times.Once);
        }

        [Fact]
        public void GetTagFoundTest()
        {
            var tag = tagController.GetTag("Sjælland");
            Assert.Equal(2, tag.Id);
            Assert.Equal(TagTypes.Region, tag.TagType);
            Assert.Equal("Sjælland", tag.Name);
        }

        [Fact]
        public void GetTagNotFoundTest()
        {
            var tag = tagController.GetTag("query");
            Assert.Equal(0, tag.Id);
            Assert.Equal(TagTypes.Ukendt, tag.TagType);
            Assert.Equal(string.Empty, tag.Name);
        }

        [Fact]
        public void GetTagsArterTest()
        {
            tagController.GetTagsArter("query");
            tagQueryQueryMock.Verify(x => x.GetTagList(), Times.Once);
        }
        
        [Fact]
        public void GetTagsArterTagFoundTest()
        {
            var list = tagController.GetTagsArter("Musvåge");
            Assert.Single(list);
        }

        [Fact]
        public void GetTagsArterTagNotFoundTest()
        {
            var list = tagController.GetTagsArter("Blå Glente");
            Assert.Empty(list);
        }

        [Fact]
        public void GetArtTagByIdTest()
        {
            tagController.GetArtTagById(1);
            artQueriesMock.Verify(x => x.GetArtTagById(1), Times.Once);
        }

        [Fact]
        public void GetArtTagByIdTestTagFound()
        {
            var tag = tagController.GetArtTagById(11);
            Assert.Equal(11, tag.Id);
            Assert.Equal(TagTypes.Art, tag.TagType);
            Assert.Equal("Drosselrørsanger", tag.Name);
        }

        [Fact]
        public void GetArtTagByIdTestTagNotFound()
        {
            var tag = tagController.GetArtTagById(10);
            Assert.Equal(0, tag.Id);
            Assert.Equal(TagTypes.Ukendt, tag.TagType);
            Assert.Equal(string.Empty, tag.Name);
        }
    }
}