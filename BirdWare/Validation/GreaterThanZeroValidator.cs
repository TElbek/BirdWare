using FluentValidation;

namespace BirdWare.Validation
{
    public class GreaterThanZeroValidator : AbstractValidator<long>
    {
        public GreaterThanZeroValidator() 
        { 
            RuleFor(value => value).GreaterThan(0);
        }
    }
}
