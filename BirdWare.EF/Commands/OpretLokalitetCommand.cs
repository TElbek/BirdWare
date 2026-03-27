using BirdWare.Domain.Entities;
using BirdWare.EF.Interfaces;

namespace BirdWare.EF.Commands
{
    public class OpretLokalitetCommand(BirdWareContext birdWareContext) : IOpretLokalitetCommand
    {
        public bool OpretLokalitet(Lokalitet lokalitet)
        {
            try
            {
                if (lokalitet != null && ExistRegion(lokalitet) && !string.IsNullOrEmpty(lokalitet.Navn))
                {
                    using var transaction = birdWareContext.Database.BeginTransaction();
                    birdWareContext.Lokalitet.Add(lokalitet);
                    birdWareContext.SaveChanges();
                    transaction.Commit();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }

        private bool ExistRegion(Lokalitet lokalitet) => birdWareContext.Region.Any(a => a.Id == lokalitet.RegionId);
    }
}