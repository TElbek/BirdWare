using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BirdWare.EF.Queries
{
    public class SynchTripQuery(BirdWareContext birdWareContext) : ContextBase(birdWareContext), ISynchTripQuery
    {
        public SynchTrip GetTrip(long fugleturId)
        {
            var trip = birdWareContext.Fugletur
                .Include(a => a.Lokalitet)
                .Include(b => b.Observationer)
                .ThenInclude(c => c.Art)
                .FirstOrDefault(r => r.Id == fugleturId);

            if (trip != null)
            {
                var synchTrip = new SynchTrip();
                synchTrip.Fugletur.Lokalitet.LokalitetNavn = trip.Lokalitet.Navn ?? string.Empty;
                synchTrip.Fugletur.Lokalitet.LokalitetId = trip.Lokalitet.Id;
                synchTrip.Fugletur.Lokalitet.Regionid = trip.Lokalitet.RegionId;
                synchTrip.Fugletur.Dato = trip.Dato ?? DateTime.MinValue;
                synchTrip.Fugletur.FugleturId = trip.Id;
                synchTrip.Fugletur.LokalitetId = trip.LokalitetId;
                synchTrip.Fugletur.Observation = trip.Observationer.Select(y => new SynchObservation { 
                                                                            ArtId = y.ArtId, 
                                                                            Beskrivelse = y.Beskrivelse ?? string.Empty }
                                                                        ).ToList();
                return synchTrip;
            }

            throw new ArgumentException();
        }
    }
}
