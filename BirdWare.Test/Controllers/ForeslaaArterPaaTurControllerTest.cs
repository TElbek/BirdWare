using BirdWare.Controllers;
using BirdWare.EF.Interfaces;
using Moq;

namespace BirdWare.Test.Controllers
{
    public class ForeslaaArterPaaTurControllerTest 
    {
        private readonly Mock<IForeslaaArterPaaTurQuery> foreslaaArterPaaTurQuery = new();
        private readonly ForeslaaArterPaaTurController foreslaaArterPaaTurController;

        public ForeslaaArterPaaTurControllerTest()
        {
            foreslaaArterPaaTurQuery.Setup(x => x.ForeslaaArterSenesteTur()).Returns([]);
            foreslaaArterPaaTurController = new ForeslaaArterPaaTurController(foreslaaArterPaaTurQuery.Object);
        }

        [Fact]
        public void ForeslaaArterTest()
        {
            foreslaaArterPaaTurController.ForeslaaArter();
            foreslaaArterPaaTurQuery.Verify(x => x.ForeslaaArterSenesteTur(), Times.Once);
        }
    }
}
