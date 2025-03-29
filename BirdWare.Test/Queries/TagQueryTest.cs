using BirdWare.Domain.Entities;
using BirdWare.EF.Queries;
using BirdWare.Test.Moq;

namespace BirdWare.Test.Queries
{
    public class TagQueryTest : DbContextMock
    {
        private readonly DbSetMock<Art> artMockSet = new();
        private readonly DbSetMock<Gruppe> gruppeMockSet = new();
        private readonly DbSetMock<Familie> familieMockSet = new();
        private readonly DbSetMock<Lokalitet> lokalitetMockSet = new();
        private readonly DbSetMock<Region> regionMockSet = new();
        private readonly DbSetMock<Fugletur> fugleturMockSet = new();

        [Fact]
        public void GetTagListTest()
        {
            var tagQuery = GetTagQueries();
            var tagList = tagQuery.GetTagList();

            Assert.Equal(26, tagList.Count);
            Assert.Equal(3, tagList.Count(q => q.TagType == Domain.Models.TagTypes.Art));
            Assert.Equal(3, tagList.Count(q => q.TagType == Domain.Models.TagTypes.Gruppe));
            Assert.Equal(3, tagList.Count(q => q.TagType == Domain.Models.TagTypes.Familie));
            Assert.Equal(3, tagList.Count(q => q.TagType == Domain.Models.TagTypes.Lokalitet));
            Assert.Equal(3, tagList.Count(q => q.TagType == Domain.Models.TagTypes.Region));
            Assert.Equal(3, tagList.Count(q => q.TagType == Domain.Models.TagTypes.Aarstal));
            Assert.Equal(3, tagList.Count(q => q.TagType == Domain.Models.TagTypes.Maaned));
            Assert.Equal(1, tagList.Count(q => q.TagType == Domain.Models.TagTypes.Land));
            Assert.Equal(1, tagList.Count(q => q.TagType == Domain.Models.TagTypes.SaesonForaar));
            Assert.Equal(1, tagList.Count(q => q.TagType == Domain.Models.TagTypes.SaesonSommer));
            Assert.Equal(1, tagList.Count(q => q.TagType == Domain.Models.TagTypes.SaesonEfteraar));
            Assert.Equal(1, tagList.Count(q => q.TagType == Domain.Models.TagTypes.SaesonVinter));
            Assert.Equal(0, tagList.Count(q => q.TagType == Domain.Models.TagTypes.Ukendt));
        }

        [Fact]
        public void GetTagListFugleturTest()
        {
            var tagQuery = GetTagQueries();
            var tagList = tagQuery.GetTagListFugletur();

            Assert.Equal(17, tagList.Count);
            Assert.Equal(0, tagList.Count(q => q.TagType == Domain.Models.TagTypes.Art));
            Assert.Equal(0, tagList.Count(q => q.TagType == Domain.Models.TagTypes.Gruppe));
            Assert.Equal(0, tagList.Count(q => q.TagType == Domain.Models.TagTypes.Familie));
            Assert.Equal(3, tagList.Count(q => q.TagType == Domain.Models.TagTypes.Lokalitet));
            Assert.Equal(3, tagList.Count(q => q.TagType == Domain.Models.TagTypes.Region));
            Assert.Equal(3, tagList.Count(q => q.TagType == Domain.Models.TagTypes.Aarstal));
            Assert.Equal(3, tagList.Count(q => q.TagType == Domain.Models.TagTypes.Maaned));
            Assert.Equal(1, tagList.Count(q => q.TagType == Domain.Models.TagTypes.Land));
            Assert.Equal(1, tagList.Count(q => q.TagType == Domain.Models.TagTypes.SaesonForaar));
            Assert.Equal(1, tagList.Count(q => q.TagType == Domain.Models.TagTypes.SaesonSommer));
            Assert.Equal(1, tagList.Count(q => q.TagType == Domain.Models.TagTypes.SaesonEfteraar));
            Assert.Equal(1, tagList.Count(q => q.TagType == Domain.Models.TagTypes.SaesonVinter));
            Assert.Equal(0, tagList.Count(q => q.TagType == Domain.Models.TagTypes.Ukendt));
        }

        private TagQuery GetTagQueries()
        {
            artMockSet.AddData([
                new() { Id = 1, Navn = "Tag1", GruppeId = 1 },
                new() { Id = 2, Navn = "Tag2", GruppeId = 2 },
                new() { Id = 3, Navn = "Tag3", GruppeId = 3 }]);

            gruppeMockSet.AddData([
                new() { Id = 1, Navn = "Gruppe1", FamilieId = 1 },
                new() { Id = 2, Navn = "Gruppe2", FamilieId = 2 },
                new() { Id = 3, Navn = "Gruppe3", FamilieId = 3 }]);

            familieMockSet.AddData([
                new() { Id = 1, Navn = "Familie1"},
                new() { Id = 2, Navn = "Familie2"},
                new() { Id = 3, Navn = "Familie3"}]);

            lokalitetMockSet.AddData([
                new() { Id = 1, Navn = "Lokalitet1", RegionId = 1 },
                new() { Id = 2, Navn = "Lokalitet2", RegionId = 2 },
                new() { Id = 3, Navn = "Lokalitet3", RegionId = 3 }]);

            regionMockSet.AddData([
                new() { Id = 1, Navn = "Region1"},
                new() { Id = 2, Navn = "Region2"},
                new() { Id = 3, Navn = "Region3"}]);

            fugleturMockSet.AddData([
                new() { Id = 1, Dato = new DateTime(2025,3,1), LokalitetId = 1 },
                new() { Id = 2, Dato = new DateTime(2023,1,1), LokalitetId = 2 },
                new() { Id = 3, Dato = new DateTime(2022,12,1), LokalitetId = 3 }]);

            MockContext.Setup(c => c.Art).Returns(artMockSet.DbSet);
            MockContext.Setup(c => c.Gruppe).Returns(gruppeMockSet.DbSet);
            MockContext.Setup(c => c.Familie).Returns(familieMockSet.DbSet);
            MockContext.Setup(c => c.Lokalitet).Returns(lokalitetMockSet.DbSet);
            MockContext.Setup(c => c.Region).Returns(regionMockSet.DbSet);
            MockContext.Setup(c => c.Fugletur).Returns(fugleturMockSet.DbSet);
            return new TagQuery(MockContext.Object);
        }
    }
}
