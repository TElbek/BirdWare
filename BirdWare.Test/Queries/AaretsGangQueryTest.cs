using BirdWare.Domain.Entities;
using BirdWare.EF.Queries;
using BirdWare.Test.Moq;

namespace BirdWare.Test.Queries
{
    public class AaretsGangQueryTest() : DbContextMock
    {
        private readonly DbSetMock<Familie> familieMockSet = new();
        private readonly DbSetMock<Gruppe> gruppeMockSet = new();
        private readonly DbSetMock<Art> artMockSet = new();
        private readonly DbSetMock<Observation> observationMockSet = new();
        private readonly DbSetMock<Fugletur> fugleturMockSet = new();
        private readonly DbSetMock<Lokalitet> lokalitetMockSet = new();

        [Fact]
        public void GetAaretsGangTest()
        { 
            var aaretsGangQuery = GetAaretsGangQuery();
            var aaretsGang = aaretsGangQuery.GetAaretsGang();
            Assert.Equal(3, aaretsGang.Count);
            Assert.Equal("Sortspætte", aaretsGang[0].ArtNavn);
            Assert.Equal("Himalayasanger", aaretsGang[1].ArtNavn);
            Assert.Equal("Rørdrum", aaretsGang[2].ArtNavn);
        }

        private AaretsGangQuery GetAaretsGangQuery()
        {
            familieMockSet.AddData(
            [
                new() { Id = 1, Navn = "Storkefugle" },
                new() { Id = 2, Navn = "Rovfugle" },
                new() { Id = 3, Navn = "Svaler og sejlere" },
                new() { Id = 4, Navn = "Sangere" },
                new() { Id = 5, Navn = "Havfugle" },
                new() { Id = 6, Navn = "Spætter" }
            ]);

            gruppeMockSet.AddData(
            [
                new() { Id = 1, Navn = "Hejrer", FamilieId = 1 },
                new() { Id = 2, Navn = "Kærhøge", FamilieId = 2 },
                new() { Id = 3, Navn = "Svaler", FamilieId = 3 },
                new() { Id = 4, Navn = "Sangere", FamilieId = 4 },
                new() { Id = 5, Navn = "Alkefugle", FamilieId = 5 }
            ]);

            artMockSet.AddData(
            [
                new() { Id = 1, GruppeId = 1, Navn = "Rørdrum", SU = false },
                new() { Id = 2, GruppeId = 2, Navn = "Himalayasanger", SU = false },
                new() { Id = 3, GruppeId = 3, Navn = "Sortspætte", SU = false },
                new() { Id = 4, GruppeId = 2, Navn = "Hedehøg", SU = false },
                new() { Id = 5, GruppeId = 1, Navn = "Lunde", SU = false }
            ]);

            observationMockSet.AddData(
            [
                new() { Id = 1, ArtId = 1, FugleturId = 1 },
                new() { Id = 2, ArtId = 2, FugleturId = 2 },
                new() { Id = 3, ArtId = 3, FugleturId = 3 },
                new() { Id = 4, ArtId = 3, FugleturId = 4 },
                new() { Id = 5, ArtId = 5, FugleturId = 5 },
                new() { Id = 6, ArtId = 6, FugleturId = 6 },
            ]);

            fugleturMockSet.AddData(
            [
                new() { Id = 1, Dato = new DateTime(DateTime.Now.Year, 1, 1), LokalitetId = 1 },
                new() { Id = 2, Dato = new DateTime(DateTime.Now.Year, 1, 2), LokalitetId = 2 },
                new() { Id = 3, Dato = new DateTime(DateTime.Now.Year, 1, 3), LokalitetId = 3 },
                new() { Id = 4, Dato = new DateTime(DateTime.Now.Year -1, 1, 4), LokalitetId = 4 },
                new() { Id = 5, Dato = new DateTime(DateTime.Now.Year -1, 1, 5), LokalitetId = 5 },
                new() { Id = 6, Dato = new DateTime(DateTime.Now.Year -1, 1, 6), LokalitetId = 6 }
            ]);

            lokalitetMockSet.AddData(
            [
                new() { Id = 1, Navn = "Lokalitet 1", RegionId = 1 },
                new() { Id = 2, Navn = "Lokalitet 2", RegionId = 2 },
                new() { Id = 3, Navn = "Lokalitet 3", RegionId = 3 },
                new() { Id = 4, Navn = "Lokalitet 4", RegionId = 4 },
                new() { Id = 5, Navn = "Lokalitet 5", RegionId = 5 },
                new() { Id = 6, Navn = "Lokalitet 6", RegionId = 6 }
            ]);

            MockContext.Setup(c => c.Familie).Returns(familieMockSet.DbSet);
            MockContext.Setup(c => c.Gruppe).Returns(gruppeMockSet.DbSet);
            MockContext.Setup(c => c.Art).Returns(artMockSet.DbSet);
            MockContext.Setup(c => c.Observation).Returns(observationMockSet.DbSet);
            MockContext.Setup(c => c.Fugletur).Returns(fugleturMockSet.DbSet);
            MockContext.Setup(c => c.Lokalitet).Returns(lokalitetMockSet.DbSet);

            var artAarQueries = new ArterAarQueries(MockContext.Object);
            return new AaretsGangQuery(MockContext.Object, artAarQueries);
        }
    }
}
