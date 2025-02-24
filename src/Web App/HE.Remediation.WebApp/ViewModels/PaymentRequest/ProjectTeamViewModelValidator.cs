using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.PaymentRequest;

public class ProjectTeamViewModelValidator : AbstractValidator<ProjectTeamViewModel>
{
    public ProjectTeamViewModelValidator()
    {
        RuleFor(x => x.MissingRoles)
            .Empty()
            .WithMessage("Add missing team members");
    }
}
