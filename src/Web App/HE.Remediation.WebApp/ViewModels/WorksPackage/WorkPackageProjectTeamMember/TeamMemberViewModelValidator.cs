using FluentValidation;
using HE.Remediation.Core.Enums;
using HE.Remediation.WebApp.CustomPropertyValidators;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageProjectTeamMember;

public class TeamMemberViewModelValidator : AbstractValidator<TeamMemberViewModel>
{
    public TeamMemberViewModelValidator()
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

        RuleFor(x => x.CompanyRegistration)
            .NotEmpty()
            .WithMessage("Enter company registration number")
            .Matches("^[a-zA-Z0-9]{8}$")
            .WithMessage("Please enter a valid Company registration number");

        RuleFor(x => x.EmailAddress)
            .NotEmpty()
            .WithMessage("Enter email address")
            .EmailAddress()
            .WithMessage(@"Enter an email address in the correct format, like name@example.com")
            .NotValidEmailAddress()
            .MaximumLength(150)
            .WithMessage("Email Address cannot exceed 150 characters");

        RuleFor(x => x.PrimaryContactNumber)
            .NotEmpty()
            .WithMessage("Enter primary contact number")
            .NotValidTelephoneGB()
            .WithMessage("Enter a Phone number, like 01632960001, 07700900982");

        When(x => x.Role == ETeamRole.Other, () =>
        {
            RuleFor(x => x.OtherRole)
                .NotEmpty()
                .WithMessage("Enter company role")
                .MaximumLength(150)
                .WithMessage("Company Role cannot exceed 150 characters");
        });

        RuleFor(x => x.ContractSigned)
            .NotNull()
            .WithMessage("Select an option - Contract Signed");

        When(x => x.Role != ETeamRole.Other, () =>
        {
            RuleFor(x => x.IndemnityInsurance)
                .NotNull()
                .WithMessage("Select an option - Indemnity Insurance");
        });

        RuleFor(x => x.InvolvedInOriginalInstallation)
            .NotNull()
            .WithMessage("Select an option - Involved In Original Installation");

        When(x => x.IndemnityInsurance == false, () =>
        {
            RuleFor(x => x.IndemnityInsuranceReason)
                .NotEmpty()
                .WithMessage("Enter reason")
                .MaximumLength(150)
                .WithMessage("Professional Indemnity Insurance reason cannot exceed 150 characters");
        });

        When(x => x.InvolvedInOriginalInstallation == true, () =>
        {
            RuleFor(x => x.InvolvedRoleReason)
                .NotEmpty()
                .WithMessage("Enter explanation for their role")
                .MaximumLength(150)
                .WithMessage("Explanation for their role cannot exceed 150 characters");
        });

        When(x => x.Role == ETeamRole.LeadContractor, () =>
        {
            RuleFor(x => x.ConsiderateConstructorSchemeType)
                .NotNull()
                .WithMessage("Select an option - Considerate Constructor Scheme Type")
                .IsInEnum()
                .WithMessage("Select an option - Considerate Constructor Scheme Type");

            RuleFor(x => x.HasChasCertification)
                .NotNull()
                .WithMessage("Select an option - Has your lead contractor obtained CHAS Elite certification");
        });
    }
}