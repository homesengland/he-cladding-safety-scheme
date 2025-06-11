using FluentValidation;
using HE.Remediation.WebApp.CustomPropertyValidators;

namespace HE.Remediation.WebApp.ViewModels.OrganisationManagement
{
    public class InviteMemberViewModelValidator : AbstractValidator<InviteMemberViewModel>
    {
        private readonly string _expectedDomain;

        public InviteMemberViewModelValidator(string adminEmail)
        {
            _expectedDomain = GetDomain(adminEmail);

            RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("Please enter First name")
            .MaximumLength(150)
            .WithMessage("First Name must be less than 150 characters")
            .Matches("^[a-zA-Z0-9 ]*$")
            .WithMessage("First name must contain only alphanumeric characters");

            RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("Please enter Last name")
            .MaximumLength(150)
            .WithMessage("Last Name must be less than 150 characters")
            .Matches("^[a-zA-Z0-9 ]*$")
            .WithMessage("Last name must contain only alphanumeric characters");

            RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Enter email address")
            .EmailAddress()
            .WithMessage(@"Enter an email address in the correct format, like name@example.com")
            .NotValidEmailAddress()
            .Must(HaveMatchingDomain)
            .WithMessage("Email domain must match applicant");

            RuleFor(x => x.ApplicationRole)
            .NotEmpty()
            .WithMessage("Enter Organisation role");
        }

        private bool HaveMatchingDomain(string email)
        {
            if(string.IsNullOrWhiteSpace(email))
            {
                return false;
            }
            return GetDomain(email).Equals(_expectedDomain, StringComparison.OrdinalIgnoreCase);
        }

        private static string GetDomain(string email)
        {
            var parts = email.Split('@');
            return parts.Length == 2 ? parts[1] : string.Empty;
        }
    }
}
