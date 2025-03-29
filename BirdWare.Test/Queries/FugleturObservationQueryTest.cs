using BirdWare.Domain.Entities;
using BirdWare.EF.Queries;
using BirdWare.Test.Moq;
using Moq;

namespace BirdWare.Test.Queries
{
    public class FugleturObservationQueryTest : DbContextMock
    {
        private readonly DbSetMock<Fugletur> fugleturMockSet = new();
        private readonly DbSetMock<Observation> observationMockSet = new();
        private readonly DbSetMock<Art> artMockSet = new();
        private readonly DbSetMock<Gruppe> gruppeMockSet = new();
        private readonly DbSetMock<Familie> familieMockSet = new();

        [Fact]
        public void GetObservationerTest()
        {
            var fugleturObservationQuery = GetFugleturObservationQuery();

            var observationer = fugleturObservationQuery.GetObservationer(1);
            
            Assert.Equal(2, observationer.Count);

            Assert.Equal(1, observationer[0].ObservationId);
            Assert.Equal("Art1", observationer[0].ArtNavn);
            Assert.Equal("Beskrivelse1", observationer[0].Bem);
            Assert.Equal(1, observationer[0].FamilieId);
            Assert.Equal("Familie1", observationer[0].FamilieNavn);
            Assert.Equal(1, observationer[0].FugleturId);
            Assert.Equal(1, observationer[0].GruppeId);
            Assert.False(observationer[0].SU);

            Assert.Equal(2, observationer[1].ObservationId);
            Assert.Equal("Art2", observationer[1].ArtNavn);
            Assert.Equal("Beskrivelse2", observationer[1].Bem);
            Assert.Equal(2, observationer[1].FamilieId);
            Assert.Equal("Familie2", observationer[1].FamilieNavn);
            Assert.Equal(1, observationer[1].FugleturId);
            Assert.Equal(2, observationer[1].GruppeId);
            Assert.True(observationer[1].SU);
        }

        [Fact]
        public void GetObservationerEmptyTest()
        {
            var idNotExists = 99;

            var fugleturObservationQuery = GetFugleturObservationQuery();
            var observationer = fugleturObservationQuery.GetObservationer(idNotExists);
            
            Assert.Empty(observationer);
        }

        [Fact]
        public void SletObservationTest()
        {
            var fugleturObservationQuery = GetFugleturObservationQuery();

            fugleturObservationQuery.SletObservation(3);

            MockContext.Verify(c => c.Observation.Remove(observationMockSet.DbSet.Single(o => o.Id == 3)), Times.Once);
            MockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        private FugleturObservationQuery GetFugleturObservationQuery()
        {
            familieMockSet.AddData([
                new() { Id = 1, Navn = "Familie1" },
                new() { Id = 2, Navn = "Familie2" },
                new() { Id = 3, Navn = "Familie3" }]);

            gruppeMockSet.AddData([
                new() { Id = 1, Navn = "Gruppe1", FamilieId = 1 },
                new() { Id = 2, Navn = "Gruppe2", FamilieId = 2 },
                new() { Id = 3, Navn = "Gruppe3", FamilieId = 3 }]);

            artMockSet.AddData([
                new() { Id = 1, Navn = "Art1", GruppeId = 1 },
                new() { Id = 2, Navn = "Art2", GruppeId = 2, SU = true },
                new() { Id = 3, Navn = "Art3", GruppeId = 3 }]);

            observationMockSet.AddData([
                new() { Id = 1, ArtId = 1, FugleturId = 1, Beskrivelse = "Beskrivelse1" },
                new() { Id = 2, ArtId = 2, FugleturId = 1, Beskrivelse = "Beskrivelse2" },
                new() { Id = 3, ArtId = 3, FugleturId = 3, Beskrivelse = "Beskrivelse3" }]);

            fugleturMockSet.AddData([
                new Fugletur { Id = 2, Dato = DateTime.Now.AddDays(-4)},
                new Fugletur { Id = 3, Dato = DateTime.Now }]);

            MockContext.Setup(c => c.Art).Returns(artMockSet.DbSet);
            MockContext.Setup(c => c.Observation).Returns(observationMockSet.DbSet);
            MockContext.Setup(c => c.Gruppe).Returns(gruppeMockSet.DbSet);
            MockContext.Setup(c => c.Familie).Returns(familieMockSet.DbSet);
            MockContext.Setup(c => c.Fugletur).Returns(fugleturMockSet.DbSet);
            return new FugleturObservationQuery(MockContext.Object);
        }
    }
}
