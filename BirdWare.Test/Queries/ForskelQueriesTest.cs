using BirdWare.Domain.Entities;
using BirdWare.EF.Queries;
using BirdWare.Test.Moq;
using Xunit.Abstractions;

namespace BirdWare.Test.Queries
{
    public class ForskelQueriesTest : DbContextMock
    {
        private readonly DbSetMock<Familie> familieMockSet = new();
        private readonly DbSetMock<Gruppe> gruppeMockSet = new();
        private readonly DbSetMock<Art> artMockSet = new();
        private readonly DbSetMock<Observation> observationMockSet = new();
        private readonly DbSetMock<Fugletur> fugleturMockSet = new();
        private readonly DbSetMock<Lokalitet> lokalitetMockSet = new();
        private readonly ITestOutputHelper testOutputHelper;

        public ForskelQueriesTest(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void GetForskelIAarTest()
        {
            var forskelQueries = GetForskelQueries();
            var forskelIAar = forskelQueries.GetForskelIAar();
            Assert.Equal("Rørdrum", forskelIAar[0].ArtNavn);
            Assert.Equal("Himalayasanger", forskelIAar[1].ArtNavn);
            Assert.Equal(2, forskelIAar.Count);
        }

        [Fact]
        public void GetForskelSidsteAarTest()
        {
            var forskelQueries = GetForskelQueries();
            var forskelSidsteAar = forskelQueries.GetForskelSidsteAar();
            Assert.Single(forskelSidsteAar);
            Assert.Equal("Sortspætte", forskelSidsteAar[0].ArtNavn);
        }

        private ForskelQueries GetForskelQueries()
        {
            familieMockSet.AddData([
                new() { Id = 1, Navn = "Storkefugle" },
                new() { Id = 2, Navn = "Rovfugle" },
                new() { Id = 3, Navn = "Svaler og sejlere" },
                new() { Id = 4, Navn = "Sangere" },
                new() { Id = 5, Navn = "Havfugle" },
                new() { Id = 6, Navn = "Spætter" }
            ]);

            gruppeMockSet.AddData([
                new() { Id = 1, Navn = "Hejrer", FamilieId = 1 },
                new() { Id = 2, Navn = "Kærhøge", FamilieId = 2 },
                new() { Id = 3, Navn = "Svaler", FamilieId = 3 },
                new() { Id = 4, Navn = "Sangere", FamilieId = 4 },
                new() { Id = 5, Navn = "Alkefugle", FamilieId = 5 },
                new() { Id = 6, Navn = "Spætter", FamilieId = 6 },
            ]);

            artMockSet.AddData([
                new() { Id = 1, GruppeId = 1, Navn = "Rørdrum", SU = false },
                new() { Id = 2, GruppeId = 2, Navn = "Rørhøg", SU = true },
                new() { Id = 3, GruppeId = 3, Navn = "Rødbrystet Svale", SU = false },
                new() { Id = 4, GruppeId = 4, Navn = "Himalayasanger", SU = false },
                new() { Id = 5, GruppeId = 5, Navn = "Polarlomvie", SU = false },
                new() { Id = 6, GruppeId = 6, Navn = "Sortspætte", SU = false },
            ]);

            observationMockSet.AddData([
                new() { Id = 1, ArtId = 1, FugleturId = 6 }, //Rørdrum i år
                new() { Id = 2, ArtId = 1, FugleturId = 4 }, //Rørdrum sidste år senere end nu

                new() { Id = 3, ArtId = 2, FugleturId = 6 }, //Rørhøg i år
                new() { Id = 4, ArtId = 2, FugleturId = 1 }, //Rørhøg sidste år - tidligere end nu

                new() { Id = 5, ArtId = 3, FugleturId = 2 }, //Rødbrystet Svale sidste år
                new() { Id = 6, ArtId = 4, FugleturId = 5 }, //Himalayasanger i år
                new() { Id = 7, ArtId = 5, FugleturId = 3 }, //Polarlomvie sidste år

                new() { Id = 8, ArtId = 6, FugleturId = 1 }  //Sortspætte sidste år
            ]);

            fugleturMockSet.AddData([
                new() { Id = 6, LokalitetId = 1, Dato = new DateTime(DateTime.Now.Year,3,25) },
                new() { Id = 5, LokalitetId = 1, Dato = new DateTime(DateTime.Now.Year,1,25) },
                new() { Id = 4, LokalitetId = 1, Dato = new DateTime(DateTime.Now.Year - 1,9,1) },
                new() { Id = 3, LokalitetId = 2, Dato = new DateTime(DateTime.Now.Year - 1,6,15) },
                new() { Id = 2, LokalitetId = 1, Dato = new DateTime(DateTime.Now.Year - 1,5,8) },
                new() { Id = 1, LokalitetId = 1, Dato = new DateTime(DateTime.Now.Year - 1,3,22) },
            ]);

            lokalitetMockSet.AddData([
                new() { Id = 1, RegionId = 1, Navn = "Danmark" },
                new() { Id = 2, RegionId = -5, Navn = "Norge" },
            ]);
            
            MockContext.Setup(c => c.Familie).Returns(familieMockSet.DbSet);
            MockContext.Setup(c => c.Gruppe).Returns(gruppeMockSet.DbSet);
            MockContext.Setup(c => c.Art).Returns(artMockSet.DbSet);
            MockContext.Setup(c => c.Observation).Returns(observationMockSet.DbSet);
            MockContext.Setup(c => c.Fugletur).Returns(fugleturMockSet.DbSet);
            MockContext.Setup(c => c.Lokalitet).Returns(lokalitetMockSet.DbSet);

            var artAarQueries = new ArterAarQueries(MockContext.Object);
            return new ForskelQueries(MockContext.Object, artAarQueries);
        }
    }
}
