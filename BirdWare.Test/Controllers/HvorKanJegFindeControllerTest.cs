using BirdWare.Controllers;
using BirdWare.EF.Interfaces;
using Moq;

namespace BirdWare.Test.Controllers
{
    public class HvorKanJegFindeControllerTest
    {
        private readonly Mock<IHvorKanJegFindeQuery> hvorKanJegFindeQueryMock = new();
        private readonly HvorKanJegFindeController hvorKanJegFindeController;

        public HvorKanJegFindeControllerTest()
        {
            hvorKanJegFindeQueryMock.Setup(x => x.GetHvorKanJegFinde()).Returns([]);
            hvorKanJegFindeController = new HvorKanJegFindeController(hvorKanJegFindeQueryMock.Object);
        }

        [Fact]
        public void GetHvorKanJegFindeTest()
        {
            hvorKanJegFindeController.HvorKanJegFinde();
            hvorKanJegFindeQueryMock.Verify(x => x.GetHvorKanJegFinde(), Times.Once);
        }
    }
}
