using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageCostsScheduling;

public class SubcontractorTeamViewModelValidator : AbstractValidator<SubcontractorTeamViewModel>
{
    public SubcontractorTeamViewModelValidator()
    {
        RuleFor(x => x.Subcontractors)
            .NotEmpty()
            .WithMessage("Add a sub-contractor");
    }
}