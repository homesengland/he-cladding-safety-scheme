using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities
{
    public class CompanyDetailsViewModelValidator : AbstractValidator<CompanyDetailsViewModel>
    {
        public CompanyDetailsViewModelValidator()
        {
            RuleFor(x => x.CompanyName)
                .NotEmpty()
                .WithMessage("Please enter a Company name")
                .MaximumLength(150)
                .WithMessage("Company name cannot exceed 150 characters");

            RuleFor(x => x.CompanyRegistrationNumber)
                .NotEmpty()
                .WithMessage("Enter the Company Registration Number")
                .MinimumLength(4)
                .WithMessage("Company Registration Number must be between 4 and 8 digits")
                .MaximumLength(8)
                .WithMessage("Company Registration Number must be between 4 and 8 digits")
                .Matches("^[a-zA-Z0-9]{4,8}$")
                .WithMessage("Company Registration Number must contain only alphanumeric characters");
        }
    }
}
