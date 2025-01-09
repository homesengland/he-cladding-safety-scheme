using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class AppointedLeadDesignerViewModelValidator : AbstractValidator<AppointedLeadDesignerViewModel>
{
    public AppointedLeadDesignerViewModelValidator()
    {
        RuleFor(x => x.LeadDesignerAppointed)
            .NotNull()
            .WithMessage("Please select Yes or No");
    }
}
