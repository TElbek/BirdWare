using BirdWare.Domain.Entities;
using BirdWare.EF.Commands;
using BirdWare.Test.Moq;
using Moq;

namespace BirdWare.Test.Commands
{
    public class OpretLokalitetCommandTest : DbContextMock
    {
        private readonly DbSetMock<Lokalitet> lokalitetMockSet = new();
        private readonly DbSetMock<Region> regionMockSet = new();

        [Fact]
        public void OpretLokalitetTest()
        {
            // Arrange
            var opretLokalitetCommand = OpretLokalitetCommandFactory();
            var lokalitet = new Lokalitet { Navn = "Test Lokalitet", RegionId = 1 };
            // Act
            opretLokalitetCommand.OpretLokalitet(lokalitet);
            // Assert
            MockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        [Fact]
        public void OpretLokalitetIngenRegionTest()
        {
            // Arrange
            var opretLokalitetCommand = OpretLokalitetCommandFactory();
            var lokalitet = new Lokalitet { Navn = "Test Lokalitet", RegionId = 0 };
            // Act
            opretLokalitetCommand.OpretLokalitet(lokalitet);
            // Assert
            MockContext.Verify(c => c.SaveChanges(), Times.Never);
        }

        private OpretLokalitetCommand OpretLokalitetCommandFactory()
        {
            lokalitetMockSet.AddData([new Lokalitet { Id = 1, Navn = "Test Lokalitet", RegionId = 1 }]);
            regionMockSet.AddData([new Region { Id = 1, Navn = "Test Region" }]);

            MockContext.Setup(c => c.Lokalitet).Returns(lokalitetMockSet.DbSet);
            MockContext.Setup(c => c.Region).Returns(regionMockSet.DbSet);
            var opretLokalitetCommand = new OpretLokalitetCommand(MockContext.Object);
            return opretLokalitetCommand;
        }

    }
}