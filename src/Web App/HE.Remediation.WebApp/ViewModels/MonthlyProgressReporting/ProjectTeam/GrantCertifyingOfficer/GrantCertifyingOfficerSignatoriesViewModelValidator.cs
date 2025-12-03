using FluentValidation;
using HE.Remediation.WebApp.CustomPropertyValidators;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.ProjectTeam.GrantCertifyingOfficer;

public class GrantCertifyingOfficerSignatoriesViewModelValidator : AbstractValidator<GrantCertifyingOfficerSignatoriesViewModel>
{
    public GrantCertifyingOfficerSignatoriesViewModelValidator()
    {
        var currentYear = DateTime.Today.Year;

        RuleFor(x => x.Signatory)
            .NotEmpty()
            .WithMessage("Enter authorised signatory")
            .MaximumLength(150)
            .WithMessage("Signatory cannot be more than 150 characters");

        RuleFor(x => x.EmailAddress)
            .NotEmpty()
            .WithMessage("Enter an email address")
            .MaximumLength(150)
            .WithMessage("Email address cannot be more than 150 characters")
            .EmailAddress()
            .WithMessage("Enter an Email address in the correct format, like name@example.com")
            .NotValidEmailAddress();

        RuleFor(x => x.DateAppointed)
            .NotEmpty()
            .WithMessage("Please enter the date of attempt.")
            .LessThanOrEqualTo(DateTime.Now)
            .WithMessage("The date of attempt cannot be in the future.");
    }

}