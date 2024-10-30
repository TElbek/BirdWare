namespace BirdWare.EF
{
    public class ContextBase(BirdWareContext birdWareContext)
    {
        protected readonly BirdWareContext birdWareContext = birdWareContext;
    }
}
