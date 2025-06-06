﻿using BirdWare.Domain.Entities;
using BirdWare.EF.Queries;
using BirdWare.Test.Moq;
using Moq;

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
            
            MockContext.Verify(c => c.Art, Times.Once);
            MockContext.Verify(c => c.Gruppe, Times.Once);
            MockContext.Verify(c => c.Familie, Times.Once);
            MockContext.Verify(c => c.Lokalitet, Times.Once);
            MockContext.Verify(c => c.Region, Times.Once);
            MockContext.Verify(c => c.Fugletur, Times.Exactly(2));
        }

        [Fact]
        public void GetTagListFugleturTest()
        {
            var tagQuery = GetTagQueries();
            var tagList = tagQuery.GetTagListFugletur();

            Assert.Equal(17, tagList.Count);

            MockContext.Verify(c => c.Art, Times.Never);
            MockContext.Verify(c => c.Gruppe, Times.Never);
            MockContext.Verify(c => c.Familie, Times.Never);
            MockContext.Verify(c => c.Lokalitet, Times.Once);
            MockContext.Verify(c => c.Region, Times.Once);
            MockContext.Verify(c => c.Fugletur, Times.Exactly(2));
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
