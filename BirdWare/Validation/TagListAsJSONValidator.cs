using FluentValidation;

namespace BirdWare.Validation
{
    public class TagListAsJSONValidator : AbstractValidator<string>
    {
        public TagListAsJSONValidator()
        {
            RuleFor(str => str).NotNull();
            RuleFor(str => str).NotEmpty();
        }
    }
}
