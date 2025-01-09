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
            .WithMessage("Enter company registration number")
            .Matches("^[a-zA-Z0-9]{8}$")
            .WithMessage("Please enter a valid Company registration number");
    }
}