using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.BuildingDetails;

public class ConfirmBuildingHeightViewModelValidator : AbstractValidator<ConfirmBuildingHeightViewModel>
{
    public ConfirmBuildingHeightViewModelValidator()
    {
        RuleFor(x => x.NumberOfStoreys)
            .NotEmpty()
            .WithMessage("Enter a number of storeys")
            .InclusiveBetween(1, 200)
            .WithMessage("Enter a number of storeys between 1 and 200");

        RuleFor(x => x.BuildingHeight)
            .NotEmpty()
            .WithMessage("Enter a height in metres")
            .InclusiveBetween(11, 1000)
            .WithMessage("Enter a height between 11 and 1000");
    }
}