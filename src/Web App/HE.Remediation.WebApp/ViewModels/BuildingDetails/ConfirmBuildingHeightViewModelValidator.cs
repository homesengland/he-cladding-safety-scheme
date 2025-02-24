using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.BuildingDetails;

public class ConfirmBuildingHeightViewModelValidator : AbstractValidator<ConfirmBuildingHeightViewModel>
{
    public ConfirmBuildingHeightViewModelValidator()
    {
        RuleFor(x => x.NumberOfStoreys)
            .NotEmpty()
            .WithMessage("Enter a number of storeys")
            .GreaterThan(0)
            .WithMessage("Enter a number of storeys")
            .LessThanOrEqualTo(999)
            .WithMessage("No more than 999 can be entered");
    }
}