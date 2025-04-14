using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;

namespace BirdWare.EF.Queries
{
    public class BrugerQuery(BirdWareContext birdWareContext) : ContextBase(birdWareContext), IBrugerQuery
    {
        public Bruger GetBrugerByName(string UserName)
        {
            if(birdWareContext.Bruger.Any(b => b.UserName == UserName))
            {
                return birdWareContext.Bruger.First(b => b.UserName == UserName);
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }
    }
}
