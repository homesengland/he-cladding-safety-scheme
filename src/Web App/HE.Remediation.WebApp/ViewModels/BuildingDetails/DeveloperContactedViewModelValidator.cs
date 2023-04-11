using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.BuildingDetails;

public class DeveloperContactedViewModelValidator : AbstractValidator<DeveloperContactedViewModel>
{
    public DeveloperContactedViewModelValidator()
    {
        RuleFor(x => x.HasDeveloperBeenContactedAboutRemediation)
            .NotNull()
            .WithMessage("Select an option");
    }
}