using FluentValidation;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.AlternativeFundingRoutes;

public class RoleForRemediationContributionViewModelValidator : AbstractValidator<RoleForRemediationContributionViewModel>
{
    public RoleForRemediationContributionViewModelValidator()
    {
        RuleFor(x => x.Roles)
            .NotEmpty()
            .WithMessage("Select an option");
    }
}