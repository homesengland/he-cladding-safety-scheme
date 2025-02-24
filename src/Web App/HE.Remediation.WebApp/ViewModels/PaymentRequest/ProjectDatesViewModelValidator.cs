using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.PaymentRequest;

public class ProjectDatesViewModelValidator : AbstractValidator<ProjectDatesViewModel>
{
    public ProjectDatesViewModelValidator()
    {
        RuleFor(x => x.ProjectDatesChanged)
            .NotNull()
            .WithMessage("Select an option");

        
    }
}
