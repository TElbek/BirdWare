using BirdWare.EF;
using Moq;

namespace BirdWare.Test
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