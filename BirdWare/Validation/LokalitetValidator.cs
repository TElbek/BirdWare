using BirdWare.Domain.Entities;
using FluentValidation;
using FluentValidation.Results;

namespace BirdWare.Validation
{
    internal class LokalitetValidator : AbstractValidator<Lokalitet>
    {
        public LokalitetValidator()
        {
            RuleFor(lokalitet => lokalitet.Navn).NotNull();
            RuleFor(lokalitet => lokalitet.Navn).NotEmpty();
            RuleFor(lokalitet => lokalitet.RegionId).NotEqual(0);
        }

        protected override bool PreValidate(ValidationContext<Lokalitet> context, ValidationResult result)
        {
            if (context.InstanceToValidate == null)
            {
                result.Errors.Add(new ValidationFailure("", "lokalitet is null"));
                return false;
            }
            return base.PreValidate(context, result);
        }
    }
}
