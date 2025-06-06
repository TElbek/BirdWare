﻿using Microsoft.EntityFrameworkCore;
using Moq;

namespace BirdWare.Test.Moq
{
    public class DbSetMock<T> where T : class
    {
        private IQueryable<T> data;
        public Mock<DbSet<T>> MockSet { get; private set; }
        public DbSet<T> DbSet => MockSet.Object;

        public DbSetMock()
        {
            data = new List<T>().AsQueryable();
            MockSet = new Mock<DbSet<T>>();
        }

        public void AddData(List<T> data)
        {
            this.data = data.AsQueryable();
            Mock();
        }

        private void Mock()
        {
            MockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
            MockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
            MockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
            MockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
        }
    }
}