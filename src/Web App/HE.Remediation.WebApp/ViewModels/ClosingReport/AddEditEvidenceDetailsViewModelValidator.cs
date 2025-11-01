using FluentValidation;
using HE.Remediation.WebApp.Extensions;

namespace HE.Remediation.WebApp.ViewModels.ClosingReport
{
    public class AddEditEvidenceDetailsViewModelValidator : AbstractValidator<AddEditEvidenceDetailsViewModel>
    {
        public AddEditEvidenceDetailsViewModelValidator()
        {
            RuleFor(x => x.ThirdPartyName)
                .NotEmpty()
                .When(x => x.Step == 1)
                .WithMessage("Please enter the third party name.")
                .MaximumLength(150)
                .When(x => x.Step == 1)
                .WithMessage("The third party name must not exceed 150 characters.");

            RuleFor(x => x.StatusOfAttempt)
                .NotNull()
                .When(x => x.Step == 1)
                .WithMessage("Please select a status of attempt.");

            RuleFor(x => x.DateOfAttempt)
                .NotEmpty()
                .When(x => x.Step == 1)
                .WithMessage("Please enter the date of attempt.")
                .LessThanOrEqualTo(DateTime.Now)
                .When(x => x.Step == 1)
                .WithMessage("The date of attempt cannot be in the future.");

            RuleFor(x => x.TypeOfContribution)
                .NotNull()
                .When(x => x.Step == 2)
                .WithMessage("Please select one type of contribution")
                .Must(x => x is { Length: 1 })
                .When(x => x.Step == 2)
                .WithMessage("Please select only one type of contribution")
                .When(x => x.Step == 2);

            RuleFor(x => x.Amount)
                .GreaterThanOrEqualTo(0M)
                .When(x => x.Step == 2)
                .WithMessage(x => "Amount must be a positive number")
                .Must(amount => new decimal?(amount).HaveNoDecimalsInAmount())
                .When(x => x.Step == 2)
                .WithMessage(x => "Amount must be a whole number");

            RuleFor(x => x.AttemptDetails)
                .NotEmpty()
                .When(x => x.Step == 2)
                .WithMessage("Please provide the attempt details")
                .MaximumLength(250)
                .When(x => x.Step == 2)
                .WithMessage("Attempt details cannot exceed 250 characters");
        }
    }
}
