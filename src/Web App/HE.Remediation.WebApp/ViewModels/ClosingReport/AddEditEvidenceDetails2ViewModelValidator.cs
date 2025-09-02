using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.ClosingReport
{
    public class AddEditEvidenceDetails2ViewModelValidator : AbstractValidator<AddEditEvidenceDetailsViewModel>
    {
        public AddEditEvidenceDetails2ViewModelValidator()
        {
            RuleFor(x => x.AttemptDetails)
                .MaximumLength(150).WithMessage("Attempt Details must not exceed 150 characters.");
        }
    }
}
