using BirdWare.Controllers;
using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using Moq;
using System.Text.Json;

namespace BirdWare.Test.Controllers
{
    public class FugleturSoegControllerTest
    {
        private readonly Mock<IFugletureByTagsQuery> fugletureByTagsQueryMock;
        private readonly Mock<IFugleturQuery> fugleturQueryMock = new();
        private readonly FugleturSoegController fugleturSoegController;

        public FugleturSoegControllerTest()
        {
            fugletureByTagsQueryMock = new Mock<IFugletureByTagsQuery>();
            fugletureByTagsQueryMock.Setup(x => x.GetFugletureByTags(It.IsAny<List<Tag>>())).Returns([]);
            fugleturQueryMock = new Mock<IFugleturQuery>();
            fugleturQueryMock.Setup(x => x.GetFugleTureAarMaaned(It.IsAny<long>(), It.IsAny<long>())).Returns([]);

            fugleturSoegController = new FugleturSoegController(fugletureByTagsQueryMock.Object, fugleturQueryMock.Object);
        }

        [Fact]
        public void GetFugletureByTagsTest()
        {
            var tagList = new List<Tag>();
            var tagListAsJson = JsonSerializer.Serialize(tagList);
            fugleturSoegController.GetFugletureByTags(tagListAsJson);
            fugletureByTagsQueryMock.Verify(x => x.GetFugletureByTags(tagList), Times.Once);
        }

        [Fact]
        public void GetFugletureAarMaanedTest()
        {
            var aarstal = 2021;
            var maaned = 1;
            fugleturSoegController.GetFugletureAarMaaned(aarstal, maaned);
            fugleturQueryMock.Verify(x => x.GetFugleTureAarMaaned(aarstal, maaned), Times.Once);
        }
    }
}
