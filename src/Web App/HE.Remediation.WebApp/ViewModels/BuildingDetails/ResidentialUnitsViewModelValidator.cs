using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.BuildingDetails
{
    public class ResidentialUnitsViewModelValidator : AbstractValidator<ResidentialUnitsViewModel>
    {
        public ResidentialUnitsViewModelValidator()
        {
            RuleFor(x => x.ResidentialUnitsCount)
                .NotEmpty()
                .WithMessage("Enter a number of residential units")
                .GreaterThan(0)
                .WithMessage("Enter a number of residential units")
                .LessThanOrEqualTo(999)
                .WithMessage("No more than 999 can be entered");

            RuleFor(x => x.NonResidentialUnits)
                .NotNull()
                .WithMessage("Select either Yes or No");
        }
    }
}
