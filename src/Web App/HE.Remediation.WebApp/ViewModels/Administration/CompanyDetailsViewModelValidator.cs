using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.Administration;

public class CompanyDetailsViewModelValidator : AbstractValidator<CompanyDetailsViewModel>
{
    public CompanyDetailsViewModelValidator()
    {
        RuleFor(e => e.CompanyName)
            .NotEmpty()
            .WithMessage("Please enter a Company name")
            .MaximumLength(150)
            .WithMessage("Company name cannot exceed 150 characters");

        RuleFor(e => e.CompanyRegistrationNumber)
            .NotEmpty()
            .WithMessage("Please enter a Company registration number")
            .Matches("^[a-zA-Z0-9]+$")
            .WithMessage("Company registration number must contain only alphanumeric characters")
            .MinimumLength(4)
            .WithMessage("Company registration number must be between 4 and 8 characters")
            .MaximumLength(8)
            .WithMessage("Company registration number must be between 4 and 8 characters");

        RuleFor(e => e.UserRoleInCompany)
            .NotEmpty()
            .WithMessage("Please enter a role")
            .MaximumLength(150)
            .WithMessage("Company role cannot exceed 150 characters");
    }
}