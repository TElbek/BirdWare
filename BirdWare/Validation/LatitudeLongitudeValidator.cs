using FluentValidation;

namespace BirdWare.Validation
{
    public class LatitudeLongitudeValidator : AbstractValidator<(double Latitude, double Longitude)>
    {
        public LatitudeLongitudeValidator()
        {
            RuleFor(x => x.Latitude).InclusiveBetween(-90, 90);
            RuleFor(x => x.Longitude).InclusiveBetween(-180, 180);
        }
    }
}
