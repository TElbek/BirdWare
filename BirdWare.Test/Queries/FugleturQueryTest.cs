using BirdWare.Domain.Entities;
using BirdWare.EF.Queries;
using BirdWare.Test.Moq;

namespace BirdWare.Test.Queries
{
    public class FugleturQueryTest : DbContextMock
    {
        private readonly DbSetMock<Fugletur> fugleturMockSet = new();
        private readonly DbSetMock<Lokalitet> lokalitetMockSet = new();
        private readonly DbSetMock<Region> regionMockSet = new();

        [Fact]
        public void GetFugleturByIdTest()
        {
            var fugleturQuery = GetFugleturQuery();

            var fugletur = fugleturQuery.GetFugletur(1);
            Assert.Equal(1, fugletur.Id);
            Assert.Equal(1, fugletur.LokalitetId);
        }

        [Fact]
        public void GetFugleturByUnknownIdGetEmptyFugleturTest()
        {
            var fugleturQuery = GetFugleturQuery();

            var idNotExist = fugleturMockSet.DbSet.Max(q => q.Id) + 1;
            var fugletur = fugleturQuery.GetFugletur(idNotExist);
            Assert.Equal(0, fugletur.Id);
            Assert.Equal(0, fugletur.LokalitetId);
        }

        [Fact]
        public void GetSenesteFugleturTest()
        {
            var fugleturQuery = GetFugleturQuery();

            var senesteFugletur = fugleturQuery.GetSenesteFugletur();
            Assert.Equal(3, senesteFugletur);
        }

        [Fact]
        public void GetFugleTureAarMaanedTest()
        {
            var fugleturQuery = GetFugleturQuery();

            var fugleTure = fugleturQuery.GetFugleTureAarMaaned(DateTime.Now.Year, DateTime.Now.Month);
            Assert.Single(fugleTure);
        }

        [Fact]
        public void GetFugleTureAarMaanedEmptyTest()
        {
            var fugleturQuery = GetFugleturQuery();

            var fugleTure = fugleturQuery.GetFugleTureAarMaaned(DateTime.Now.Year + 1, DateTime.Now.Month + 1);
            Assert.Empty(fugleTure);
        }

        private FugleturQuery GetFugleturQuery()
        { 
            fugleturMockSet.AddData([
                new() { Id = 1, Dato = DateTime.Now, LokalitetId = 1},
                new() { Id = 2, Dato = DateTime.Now.AddMonths(-1), LokalitetId = 2},
                new() { Id = 3, Dato = DateTime.Now.AddMonths(+1), LokalitetId = 3}]);

            lokalitetMockSet.AddData([
                new() { Id = 1, Navn = "Lokalitet1", RegionId = 1},
                new() { Id = 2, Navn = "Lokalitet2", RegionId = 2},
                new() { Id = 3, Navn = "Lokalitet3", RegionId = 3}]);

            regionMockSet.AddData([
                new() { Id = 1, Navn = "Region1"},
                new() { Id = 2, Navn = "Region2"},
                new() { Id = 3, Navn = "Region3"}]);

            MockContext.Setup(c => c.Fugletur).Returns(fugleturMockSet.DbSet);
            MockContext.Setup(c => c.Lokalitet).Returns(lokalitetMockSet.DbSet);
            MockContext.Setup(c => c.Region).Returns(regionMockSet.DbSet);

            return new FugleturQuery(MockContext.Object);
        }
    }
}
