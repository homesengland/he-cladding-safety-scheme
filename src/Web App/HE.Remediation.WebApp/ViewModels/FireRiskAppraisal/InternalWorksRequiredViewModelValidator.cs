using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAppraisal;

public class InternalWorksRequiredViewModelValidator: AbstractValidator<InternalWorksRequiredViewModel>
{
    public InternalWorksRequiredViewModelValidator()
    {
        RuleFor(x => x.WorksRequired)
            .NotEmpty()
            .WithMessage("Select an option");
    }
}
