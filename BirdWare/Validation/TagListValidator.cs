using BirdWare.Domain.Models;
using FluentValidation;

namespace BirdWare.Validation
{
    public class TagListValidator : AbstractValidator<List<Tag>>
    {
        public TagListValidator()
        {
            RuleFor(list => list).NotNull();
            RuleFor(list => list).NotEmpty();
            RuleForEach(list => list).SetValidator(new TagValidator());
        }
    }

    public class TagValidator : AbstractValidator<Tag>
    {
        public TagValidator()
        {
            RuleFor(tag => tag.Id).GreaterThan(0);
            RuleFor(tag => tag.TagType).IsInEnum();
            RuleFor(tag => tag.Name).NotEmpty();
        }
    }
}
