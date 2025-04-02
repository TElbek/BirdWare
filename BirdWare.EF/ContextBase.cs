using System.Diagnostics.CodeAnalysis;

namespace BirdWare.EF
{
    [ExcludeFromCodeCoverage]
    public class ContextBase(BirdWareContext birdWareContext)
    {
        protected readonly BirdWareContext birdWareContext = birdWareContext;
    }
}
