using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.BuildingDetails;

public class ResponsibleForTheRemediationViewModelValidator : AbstractValidator<ResponsibleForTheRemediationViewModel>
{
    public ResponsibleForTheRemediationViewModelValidator()
    {
        RuleFor(x => x.BuildingRemediationResponsibilityType)
            .NotNull()
            .WithMessage("Select an option");
    }
}