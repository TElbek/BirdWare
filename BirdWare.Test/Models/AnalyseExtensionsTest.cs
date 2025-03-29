using BirdWare.Domain.Models;

namespace BirdWare.Test.Models
{
    public class AnalyseExtensionsTest
    {
        [Fact]
        public void GetDescriptionTest()
        {
            Assert.NotEqual(AnalyseTyper.FoersteObsIDatabasen.ToString(), AnalyseTyper.FoersteObsIDatabasen.GetDescription());
            Assert.NotEqual(AnalyseTyper.FoersteObsIDK.ToString(), AnalyseTyper.FoersteObsIDK.GetDescription());
            Assert.NotEqual(AnalyseTyper.FoersteObsIRegion.ToString(), AnalyseTyper.FoersteObsIRegion.GetDescription());
            Assert.NotEqual(AnalyseTyper.FoersteObsForLokalitet.ToString(), AnalyseTyper.FoersteObsForLokalitet.GetDescription());
            Assert.NotEqual(AnalyseTyper.FoersteObsIAar.ToString(), AnalyseTyper.FoersteObsIAar.GetDescription());
            Assert.NotEqual(AnalyseTyper.FoersteObsIMaaned.ToString(), AnalyseTyper.FoersteObsIMaaned.GetDescription());

            Assert.NotEqual(string.Empty, AnalyseTyper.FoersteObsIDatabasen.GetDescription());
            Assert.NotEqual(string.Empty, AnalyseTyper.FoersteObsIDK.GetDescription());
            Assert.NotEqual(string.Empty, AnalyseTyper.FoersteObsIRegion.GetDescription());
            Assert.NotEqual(string.Empty, AnalyseTyper.FoersteObsForLokalitet.GetDescription());
            Assert.NotEqual(string.Empty, AnalyseTyper.FoersteObsIAar.GetDescription());
            Assert.NotEqual(string.Empty, AnalyseTyper.FoersteObsIMaaned.GetDescription());
        }
    }
}
