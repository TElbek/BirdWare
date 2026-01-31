using BirdWare.Domain.Entities;
using BirdWare.EF.Commands;
using BirdWare.Test.Moq;
using Moq;

namespace BirdWare.Test.Commands
{
    public class SletObsCommandTest : DbContextMock
    {
        private readonly DbSetMock<Observation> observationMockSet = new();
        private readonly DbSetMock<Fugletur> fugleturMockSet = new();

        [Fact]
        public void SletObservationTest()
        {
            var sletObsCommand = GetSletObsCommand();

            sletObsCommand.SletObservation(3);

            MockContext.Verify(c => c.Observation.Remove(observationMockSet.DbSet.Single(o => o.Id == 3)), Times.Once);
            MockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        [Fact]
        public void SletObservationIkkeFundetTest()
        {
            var sletObsCommand = GetSletObsCommand();

            sletObsCommand.SletObservation(33);

            MockContext.Verify(c => c.Observation.Remove(It.IsAny<Observation>()), Times.Never);
            MockContext.Verify(c => c.SaveChanges(), Times.Never);
        }

        private SletObsCommand GetSletObsCommand()
        {
            observationMockSet.AddData([
                new() { Id = 1, ArtId = 1, FugleturId = 1, Beskrivelse = "Beskrivelse1" },
                new() { Id = 2, ArtId = 2, FugleturId = 1, Beskrivelse = "Beskrivelse2" },
                new() { Id = 3, ArtId = 3, FugleturId = 3, Beskrivelse = "Beskrivelse3" }]);

            fugleturMockSet.AddData([
                new Fugletur { Id = 2, Dato = DateTime.Now.AddDays(-4)},
                new Fugletur { Id = 3, Dato = DateTime.Now }]);

            MockContext.Setup(c => c.Observation).Returns(observationMockSet.DbSet);
            MockContext.Setup(c => c.Fugletur).Returns(fugleturMockSet.DbSet);
            return new SletObsCommand(MockContext.Object);
        }
    }
}
