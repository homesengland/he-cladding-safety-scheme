using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAppraisal;

public class InternalWorksDetailViewModelValidator: AbstractValidator<InternalWorksDetailViewModel>
{
    public InternalWorksDetailViewModelValidator()
        {
            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Enter a description")
                .MaximumLength(200)
                .WithMessage("Description cannot exceed 200 characters");
    }
}
    