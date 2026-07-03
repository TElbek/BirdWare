using BirdWare.Domain.Models;
using FluentValidation;
using System.Data;

namespace BirdWare.Validation
{
    public class OpdaterObsValidator : AbstractValidator<VObs>
    {
        public OpdaterObsValidator()
        {
            RuleFor(vObs => vObs).NotNull();
            RuleFor(vObs => vObs.ObservationId).GreaterThan(0);
            RuleFor(vObs => vObs.ArtId).GreaterThan(0);
            RuleFor(vObs => vObs.FugleturId).GreaterThan(0);
        }
    }
}
