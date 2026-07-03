using FluentValidation;

namespace BirdWare.Validation
{
    public class AarMaanedValidator : AbstractValidator<(long Aarstal, long Maaned)>
    {
        public AarMaanedValidator()
        {
           RuleFor(x => x.Aarstal).InclusiveBetween(1994, DateTime.Now.Year);
           RuleFor(x => x.Maaned).InclusiveBetween(1, 12);
        }
    }
}
