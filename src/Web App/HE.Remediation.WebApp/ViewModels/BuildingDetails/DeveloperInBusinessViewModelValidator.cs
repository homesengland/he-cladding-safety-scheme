using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.BuildingDetails;

public class DeveloperInBusinessViewModelValidator : AbstractValidator<DeveloperInBusinessViewModel>
{
    public DeveloperInBusinessViewModelValidator()
    {
        RuleFor(x => x.IsOriginalDeveloperStillInBusiness)
            .NotNull()
            .WithMessage("Select an option");
    }
}