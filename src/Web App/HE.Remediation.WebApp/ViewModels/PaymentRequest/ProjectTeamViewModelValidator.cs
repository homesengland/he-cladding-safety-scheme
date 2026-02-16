using FluentValidation;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.PaymentRequest;

public class ProjectTeamViewModelValidator : AbstractValidator<ProjectTeamViewModel>
{
    public ProjectTeamViewModelValidator()
    {
        RuleFor(x => x.MissingRoles)
            .Must(x => x is null || !x.Any(role => role != ETeamRole.ManagingAgent))
            .WithMessage("Add missing team members");
    }
}
