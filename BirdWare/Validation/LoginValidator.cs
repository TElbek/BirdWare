using BirdWare.Domain.Models;
using FluentValidation;

namespace BirdWare.Validation
{
    public class LoginValidator : AbstractValidator<LoginModel>
    {
        public LoginValidator()
        {
            RuleFor(login => login)
                .NotNull();

            RuleFor(x => x.Username)
                .NotEmpty()
                .Length(3, 50);

            RuleFor(x => x.Password)
                .NotEmpty()
                .Length(3, 100);
        }
    }
}
