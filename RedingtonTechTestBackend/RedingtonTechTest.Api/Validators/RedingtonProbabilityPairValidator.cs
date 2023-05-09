using FluentValidation;
using RedingtonTechTest.Api.Requests;

namespace RedingtonTechTest.Api.Validators
{
    public class RedingtonProbabilityPairValidator : AbstractValidator<CalculateProbabilityRequest>
    {
        public RedingtonProbabilityPairValidator()
        {
            RuleFor(x => x.FirstValue)
                .InclusiveBetween(0.0f, 1.0f)
                .WithMessage("probability value must be between 0 and 1");
            RuleFor(x => x.SecondValue)
                .InclusiveBetween(0.0f, 1.0f)
                .WithMessage("probability value must be between 0 and 1");
            RuleFor(x => x.CalculationType)
                .Must(x => Enum.IsDefined(typeof(RedingtonCalculationTypes), x)).WithMessage("invalid calc type");
        }
    }
}
