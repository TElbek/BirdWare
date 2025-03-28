using BirdWare.EF;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace BirdWare.Test
{
    public class QueriesTestBase<T> where T : class
    {
        protected Mock<DbSet<T>> MockSet { get; set; }
        protected IQueryable<T> Data { get; set; }

        protected Mock<BirdWareContext> MockContext { get; set; }

        public QueriesTestBase()
        {
            MockContext = new Mock<BirdWareContext>();
            Data = new List<T>().AsQueryable();
            MockSet = new Mock<DbSet<T>>();
        }

        protected void AddData(List<T> data)
        {
            Data = data.AsQueryable();
            Mock();
        }

        private void Mock()
        {
            MockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(Data.Provider);
            MockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(Data.Expression);
            MockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(Data.ElementType);
            MockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(Data.GetEnumerator());
        }
    }
}