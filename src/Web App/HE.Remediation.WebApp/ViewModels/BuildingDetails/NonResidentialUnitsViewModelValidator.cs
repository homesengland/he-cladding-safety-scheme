using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.BuildingDetails
{
    public class NonResidentialUnitsViewModelValidator : AbstractValidator<NonResidentialUnitsViewModel>
    {
        public NonResidentialUnitsViewModelValidator()
        {
            RuleFor(x => x.NonResidentialUnitsCount)
                .NotEmpty()
                .WithMessage("Enter a number of residential units")
                .GreaterThan(0)
                .WithMessage("Enter a number of residential units")
                .LessThan(999)
                .WithMessage("No more than 999 can be entered");
        }
    }
}
