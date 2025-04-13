using BirdWare.Domain.Entities;
using BirdWare.Domain.Models;
using BirdWare.EF.Commands;
using BirdWare.Test.Moq;
using Moq;

namespace BirdWare.Test.Commands
{
    public class OpdaterObsCommandTest : DbContextMock
    {
        private readonly DbSetMock<Observation> observationMockSet = new();

        [Fact]
        public void OpdaterObservationTest()
        {
            Observation observation = CreateObservation();

            var opdaterObsCommand = GetOpdaterObsCommand(observation);
            VObs vObs = CreateVObs();

            var result = opdaterObsCommand.OpdaterObservation(vObs);
            Assert.True(result);
            Assert.Equal("New description", observation.Beskrivelse);
            MockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        [Fact]
        public void OpdaterObservationTestWithUnknownId()
        {
            Observation observation = CreateObservation();
            var opdaterObsCommand = GetOpdaterObsCommand(observation);
            VObs vObs = CreateVObs();

            vObs.ObservationId = 2;
            var result = opdaterObsCommand.OpdaterObservation(vObs);
            Assert.False(result);
            Assert.Equal("Old description", observation.Beskrivelse);
            MockContext.Verify(c => c.SaveChanges(), Times.Never);
        }

        private static VObs CreateVObs()
        {
            return new VObs
            {
                ObservationId = 1,
                Bem = "New description"
            };
        }

        private static Observation CreateObservation()
        {
            return new Observation
            {
                Id = 1,
                Beskrivelse = "Old description"
            };
        }

        private OpdaterObsCommand GetOpdaterObsCommand(Observation observation)
        {
            observationMockSet.AddData([observation]);
            MockContext.Setup(c => c.Observation).Returns(observationMockSet.DbSet);
            return new OpdaterObsCommand(MockContext.Object);
        }
    }
}