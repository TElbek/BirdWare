using BirdWare.Domain.Entities;
using BirdWare.EF.Geography;
using BirdWare.EF.Interfaces;

namespace BirdWare.EF.Commands
{
    public class OpretLokalitetCommand(BirdWareContext birdWareContext) : IOpretLokalitetCommand
    {
        public bool OpretLokalitet(Lokalitet lokalitet)
        {
            if (!ExistRegion(lokalitet)) return false;

            try
            {
                lokalitet.Id = GetNewId();
                if (lokalitet.Latitude != null && lokalitet.Longitude != null)
                {
                    lokalitet.Point = GeographyPoint.GetPointFromLatLong(lokalitet.Latitude ?? 0, lokalitet.Longitude ?? 0);
                }

                birdWareContext.Lokalitet.Add(lokalitet);
                birdWareContext.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private long GetNewId()
        {
            return birdWareContext.Lokalitet.Any() ? 
                   birdWareContext.Lokalitet.Max(a => a.Id) + 1 : 1;
        }

        private bool ExistRegion(Lokalitet lokalitet) => birdWareContext.Region.Any(a => a.Id == lokalitet.RegionId);
    }
}