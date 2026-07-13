using BirdWare.Controllers;
using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using BirdWare.Interfaces;
using Moq;

namespace BirdWare.Test.Controllers
{
    public class TagControllerTest
    {
        private readonly Mock<ITagHandler> tagHandlerMock = new();
        private readonly Mock<IArtQueries> artQueriesMock = new();
        private readonly Mock<ISoegArtIkkeSetPaaTurHandler> soegArtIkkeSetPaaTurHandlerMock = new();

        private readonly TagController tagController;

        public TagControllerTest()
        {
            tagHandlerMock.Setup(x => x.GetTagList(It.IsAny<string>())).Returns((string query) =>
            {
                if (query == "Musvåge") { return [new TagGroup {Name = "Art", Tags = [new() { Id = 1, Name = "Musvåge", TagType = TagTypes.Art }]}];}
                return [];
            });

            tagHandlerMock.Setup(x => x.GetTagListFugletur(It.IsAny<string>())).Returns((string query) =>
            {
                if (query == "Vestamager") { return [new TagGroup { Name = "Lokalitet", Tags = [new() { Id = 1, Name = "Vestamager", TagType = TagTypes.Lokalitet }] }]; }
                return [];
            });

            tagHandlerMock.Setup(x => x.GetTag(It.IsAny<string>())).Returns((string query) =>
            {
                if (query == "Sjælland") { return new Tag { Id = 2, Name = "Sjælland", TagType = TagTypes.Region }; }
                return new Tag();
            });

            tagHandlerMock.Setup(x => x.GetTagListArt(It.IsAny<string>())).Returns((string query) =>
            {
                if (query == "Musvåge") { return [new Tag { Id = 1, Name = "Musvåge", TagType = TagTypes.Art }]; }
                return [];
            });

            artQueriesMock.Setup(x => x.GetArtTagById(It.IsAny<long>())).Returns((long id) =>
            {
                if (id == 11) { return new Tag { Id = 11, Name = "Drosselrørsanger", TagType = TagTypes.Art }; }
                return new Tag();
            });

            soegArtIkkeSetPaaTurHandlerMock.Setup(x => x.Handle(It.IsAny<string>())).Returns((string query) =>
            {
                if (query == "Musvåge") { return [new Tag { Id = 1, Name = "Musvåge", TagType = TagTypes.Art }]; }
                return [];
            });

            tagController = new TagController(
                tagHandlerMock.Object, 
                artQueriesMock.Object, 
                soegArtIkkeSetPaaTurHandlerMock.Object);
        }

        [Fact]
        public void GetTagListTest()
        {
            tagController.GetTagList("query");
            tagHandlerMock.Verify(x => x.GetTagList(It.IsAny<string>()), Times.Once);
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
            tagHandlerMock.Verify(x => x.GetTagListFugletur(It.IsAny<string>()), Times.Once);
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
            tagController.GetTag("Sjælland");
            tagHandlerMock.Verify(x => x.GetTag(It.IsAny<string>()), Times.Once);
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
            tagController.GetTagsArterIkkeSetPaaTur("Musvåge");
            soegArtIkkeSetPaaTurHandlerMock.Verify(x => x.Handle(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void GetTagsArterTagFoundTest()
        {
            var list = tagController.GetTagsArterIkkeSetPaaTur("Musvåge");
            Assert.Single(list);
        }

        [Fact]
        public void GetTagsArterTagNotFoundTest()
        {
            var list = tagController.GetTagsArterIkkeSetPaaTur("Markpiber");
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