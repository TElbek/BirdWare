using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using BirdWare.Interfaces;
using BirdWare.Tools;
using Moq;

namespace BirdWare.Test.Mcp
{
    public class ObservationArtToolTests
    {
        [Fact]
        public async Task GetOrderStatus_ResolvesSpeciesTag_AndReturnsMatchingObservations()
        {
            // Arrange
            const string speciesName = "Solsort";

            var speciesTag = new Tag
            {
                Id = 42,
                Name = speciesName,
                TagType = TagTypes.Art
            };

            var expectedObservations = new List<VObs>
            {
                new()
                {
                    ObservationId = 1,
                    ArtNavn = speciesName
                },
                new()
                {
                    ObservationId = 2,
                    ArtNavn = speciesName
                }
            };

            var tagHandler = new Mock<ITagHandler>();
            tagHandler
                .Setup(x => x.GetTag(speciesName))
                .Returns(speciesTag);

            var observationsQuery = new Mock<IObservationsByTagsQuery>();
            observationsQuery
                .Setup(x => x.GetByTags(It.Is<IEnumerable<Tag>>(
                    tags => tags.Single() == speciesTag)))
                .Returns(expectedObservations);

            // Act
            var result = ObservationArtTool.GetObservationerAfArt(
                tagHandler.Object,
                observationsQuery.Object,
                speciesName);

            // Assert
            Assert.Equal(expectedObservations, result);

            tagHandler.Verify(
                x => x.GetTag(speciesName),
                Times.Once);

            observationsQuery.Verify(
                x => x.GetByTags(It.Is<IEnumerable<Tag>>(
                    tags => tags.Single() == speciesTag)),
                Times.Once);
        }
    }
}
