using BirdWare.Controllers;
using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using Moq;
using System.Text.Json;

namespace BirdWare.Test.Controllers
{
    public class ObservationSoegControllerTest
    {
        private readonly Mock<IObservationsByTagsQuery> observationsByTagsQueryMock = new();
        private readonly ObservationSoegController observationSoegController;

        public ObservationSoegControllerTest()
        {
            observationsByTagsQueryMock.Setup(o => o.GetObservationsByTags(It.IsAny<List<Tag>>())).Returns([]);
            observationSoegController = new ObservationSoegController(observationsByTagsQueryMock.Object);
        }

        [Fact]
        public void GetObservationsByTagsTest()
        {
            var tagList = new List<Tag>();
            var tagListAsJson = JsonSerializer.Serialize(tagList);
            observationSoegController.GetObservationsByTags(tagListAsJson);
            observationsByTagsQueryMock.Verify(o => o.GetObservationsByTags(tagList), Times.Once);
        }
    }
}
