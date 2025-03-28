using BirdWare.EF;
using Moq;

namespace BirdWare.Test.Framework
{
    public class DbContextMock
    {
        protected Mock<BirdWareContext> MockContext { get; private set; }

        public DbContextMock()
        {
            MockContext = new Mock<BirdWareContext>();
        }
    }
}