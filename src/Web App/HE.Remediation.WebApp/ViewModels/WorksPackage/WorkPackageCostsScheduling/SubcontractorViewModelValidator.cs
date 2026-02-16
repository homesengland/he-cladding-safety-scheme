using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageCostsScheduling;

public class SubcontractorViewModelValidator : AbstractValidator<SubcontractorViewModel>
{
    public SubcontractorViewModelValidator()
    {
        RuleFor(x => x.CompanyName)
            .NotEmpty()
            .WithMessage("Enter company name")
            .MaximumLength(150)
            .WithMessage("Company Name cannot exceed 150 characters");

        RuleFor(x => x.CompanyRegistration)
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