using BirdWare.Domain.Models;
using FluentValidation;

namespace BirdWare.Validation
{
    public class SynchTripValidator : AbstractValidator<SynchTrip>
    {
        public SynchTripValidator()
        {
            RuleFor(x => x.Fugletur.FugleturId).GreaterThan(0);
            RuleFor(x => x.Fugletur.LokalitetId).GreaterThan(0);
            RuleFor(x => x.Fugletur.Lokalitet.LokalitetId).GreaterThan(0);
            RuleFor(x => x.Fugletur.Lokalitet.Regionid).NotEqual(0);
            RuleForEach(x => x.Fugletur.Observation).SetValidator(new ObservationValidator());
        }
    }

    public class ObservationValidator : AbstractValidator<SynchObservation>
    {
        public ObservationValidator()
        {
            RuleFor(x => x.ArtId).GreaterThan(0);
        }
    }
}
