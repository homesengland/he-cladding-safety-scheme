using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities;

public class ClaimingGrantViewModelValidator : AbstractValidator<ClaimingGrantViewModel>
{
    public ClaimingGrantViewModelValidator()
    {
        RuleFor(x => x.IsClaimingGrant)
            .NotNull()
            .WithMessage("Select an option");
    }
}