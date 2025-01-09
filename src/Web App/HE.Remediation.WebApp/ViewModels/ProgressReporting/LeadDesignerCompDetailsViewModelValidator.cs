using FluentValidation;
using HE.Remediation.Core.Enums;
using HE.Remediation.WebApp.CustomPropertyValidators;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class LeadDesignerCompDetailsViewModelValidator : AbstractValidator<LeadDesignerCompDetailsViewModel>
{
    public LeadDesignerCompDetailsViewModelValidator()
    {
        RuleFor(x => x.CompanyName)
                .NotEmpty()
                .WithMessage("Please enter a Company name")
                .MaximumLength(150)
                .WithMessage("Company name cannot exceed 150 characters");

        RuleFor(x => x.CompanyRegistrationNumber)
            .NotEmpty()
            .WithMessage("Please enter a Company registration number")
            .Matches("^[a-zA-Z0-9]{8}$")
            .WithMessage("Please enter a valid Company registration number");

        RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Please enter a Lead Designer name")
                .MaximumLength(150)
                .WithMessage("Lead Designer name cannot exceed 150 characters");
            
        RuleFor(x => x.EmailAddress)
            .NotEmpty()
            .WithMessage("Please enter an Email address")
            .EmailAddress()
            .WithMessage(@"Enter an Email address in the correct format, like name@example.com")
            .NotValidEmailAddress();

        RuleFor(x => x.PrimaryContactNumber)
            .NotEmpty()
            .WithMessage("Please enter a Primary Contact number")
            .NotValidTelephoneGB()
            .WithMessage("Enter a Phone number, like 01632960001, 07700900982");

        RuleFor(x => x.ContractSigned)
            .NotNull()
            .WithMessage("Select an option - Contract Signed");

        RuleFor(x => x.IndemnityInsurance)
            .NotNull()
            .WithMessage("Select an option - Indemnity Insurance");

        RuleFor(x => x.LeadDesignerInvolvedInOriginalInstallation)
            .NotNull()
            .WithMessage("Select an option - Lead Designer Involved In Original Installation");

        When(x => x.IndemnityInsurance == ENoYes.No, () =>
        {
            RuleFor(x => x.IndemnityInsuranceReason)                
                .NotEmpty()
                .WithMessage("Please provide a reason")
                .MaximumLength(150)
                .WithMessage("The reason cannot exceed 150 characters");                
        });

        When(x => x.LeadDesignerInvolvedInOriginalInstallation == ENoYes.Yes, () =>
        {
            RuleFor(x => x.LeadDesignerInvolvedRoleReason)                
                .NotEmpty()
                .WithMessage("Please explain their role")
                .MaximumLength(150)
                .WithMessage("Explanation for their role cannot exceed 150 characters");
        });
    }
}
