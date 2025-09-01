using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.BuildingDetails;

public class WorksAlreadyCompletedViewModelValidator : AbstractValidator<WorksAlreadyCompletedViewModel>
{
    public WorksAlreadyCompletedViewModelValidator()
    {
        RuleFor(x => x.WorksAlreadyCompleted)
            .NotNull()
            .WithMessage("Select an option");
    }
}