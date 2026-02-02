using FluentValidation;
using HE.Remediation.WebApp.CustomPropertyValidators;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageGrantCertifyingOfficer;

public class DetailsViewModelValidator : AbstractValidator<DetailsViewModel>
{
    public DetailsViewModelValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Enter name")
            .MaximumLength(150)
            .WithMessage("Name cannot exceed 150 characters");

        RuleFor(x => x.CompanyName)
            .NotEmpty()
            .WithMessage("Enter company name")
            .MaximumLength(150)
            .WithMessage("Company Name cannot exceed 150 characters");

        RuleFor(x => x.CompanyRegistrationNumber)
            .NotEmpty()
            .WithMessage("Enter the Company Registration Number")
            .MinimumLength(4)
            .WithMessage("Company Registration Number must be between 4 and 8 digits")
            .MaximumLength(8)
            .WithMessage("Company Registration Number must be between 4 and 8 digits")
            .Matches("^[a-zA-Z0-9]{4,8}$")
            .WithMessage("Company Registration Number must contain only alphanumeric characters");

        RuleFor(x => x.EmailAddress)
            .NotEmpty()
            .WithMessage("Enter email address")
            .EmailAddress()
            .WithMessage(@"Enter an email address in the correct format, like name@example.com")
            .NotValidEmailAddress()
            .MaximumLength(150)
            .WithMessage("Email address cannot exceed 150 characters");

        RuleFor(x => x.PrimaryContactNumber)
            .NotEmpty()
            .WithMessage("Enter primary contact number")
            .NotValidTelephoneGB()
            .WithMessage("Enter a Phone number, like 01632960001, 07700900982");

        RuleFor(x => x.ContractSigned)
            .NotNull()
            .WithMessage("Select an option - Contract Signed");

        RuleFor(x => x.IndemnityInsurance)
            .NotNull()
            .WithMessage("Select an option - Indemnity Insurance");

        When(x => x.IndemnityInsurance == false, () =>
        {
            RuleFor(x => x.IndemnityInsuranceReason)
                .NotEmpty()
                .WithMessage("Enter reason")
                .MaximumLength(150)
                .WithMessage("Professional Indemnity Insurance reason cannot exceed 150 characters");
        });

        RuleFor(x => x.InvolvedInOriginalInstallation)
            .NotNull()
            .WithMessage("Select an option - Involved In Original Installation");

        When(x => x.InvolvedInOriginalInstallation == true, () =>
        {
            RuleFor(x => x.InvolvedRoleReason)
                .NotEmpty()
                .WithMessage("Enter explanation for their role")
                .MaximumLength(150)
                .WithMessage("Explanation for their role cannot exceed 150 characters");
        });

    }
}
