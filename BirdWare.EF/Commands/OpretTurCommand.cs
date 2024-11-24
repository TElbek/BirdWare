using BirdWare.Domain.Entities;
using BirdWare.EF.Interfaces;

namespace BirdWare.EF.Commands
{
    public class OpretTurCommand(BirdWareContext birdWareContext) : ContextBase(birdWareContext), IOpretTurCommand
    {
        public bool OpretTurPaaLokalitet(long lokalitetId)
        {
            var existLokalitet = birdWareContext.Lokalitet.Any(a => a.Id == lokalitetId);
            if (existLokalitet)
            {
                var newFugleturId = birdWareContext.Fugletur.Max(a => a.Id) + 1;
                var lokalitet = birdWareContext.Lokalitet.First(a => a.Id == lokalitetId);

                var fugletur = new Fugletur() {Id = newFugleturId, Dato = DateTime.Now, LokalitetId = lokalitetId , Lokalitet = lokalitet};
                try
                {
                    birdWareContext.Fugletur.Add(fugletur);
                    birdWareContext.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }            
            return false;
        }
    }
}
