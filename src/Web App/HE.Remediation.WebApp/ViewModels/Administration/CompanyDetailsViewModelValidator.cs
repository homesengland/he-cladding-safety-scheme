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
            .MaximumLength(150)
            .WithMessage("Company registration number cannot exceed 150 characters");

        RuleFor(e => e.UserRoleInCompany)
            .NotEmpty()
            .WithMessage("Please enter a role")
            .MaximumLength(150)
            .WithMessage("Company role cannot exceed 150 characters");
    }
}