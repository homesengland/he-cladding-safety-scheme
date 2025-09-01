using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.ClosingReport
{
    public class AddEditEvidenceDetailsViewModelValidator : AbstractValidator<AddEditEvidenceDetailsViewModel>
    {
        public AddEditEvidenceDetailsViewModelValidator()
        {
            RuleFor(x => x.ThirdPartyName)
                .NotEmpty().WithMessage("Please enter the third party name.")
                .MaximumLength(150).WithMessage("The third party name must not exceed 150 characters.");
            RuleFor(x => x.StatusOfAttempt)
                .NotNull().WithMessage("Please select a status of attempt.");
            RuleFor(x => x.DateOfAttempt)
                .NotEmpty().WithMessage("Please enter the date of attempt.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("The date of attempt cannot be in the future.");
        }
    }
}
