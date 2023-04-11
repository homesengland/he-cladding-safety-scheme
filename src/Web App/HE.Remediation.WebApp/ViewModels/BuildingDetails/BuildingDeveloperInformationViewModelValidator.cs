using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.BuildingDetails;

public class BuildingDeveloperInformationViewModelValidator : AbstractValidator<BuildingDeveloperInformationViewModel>
{
    public BuildingDeveloperInformationViewModelValidator()
    {
        RuleFor(x => x.DoYouKnowOriginalDeveloper)
            .NotNull()
            .WithMessage("Select an option");        
    }
}