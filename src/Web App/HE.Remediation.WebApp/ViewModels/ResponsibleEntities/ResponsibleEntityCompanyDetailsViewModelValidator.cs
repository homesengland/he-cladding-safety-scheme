using FluentValidation;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities
{
    public class ResponsibleEntityCompanyDetailsViewModelValidator : AbstractValidator<ResponsibleEntityCompanyDetailsViewModel>
    {
        public ResponsibleEntityCompanyDetailsViewModelValidator()
        {
            // Rules for non-ResponsibleActorsScheme
            When(x => x.ApplicationScheme != EApplicationScheme.ResponsibleActorsScheme, () => {
                RuleFor(x => x.CompanyName)
                    .NotEmpty()
                    .WithMessage("Please enter a Company name")
                    .MaximumLength(150)
                    .WithMessage("Company name cannot exceed 150 characters");

                RuleFor(x => x.CompanyRegistrationNumber)
                    .NotEmpty()
                    .WithMessage("Please enter a Company registration number")
                    .Matches("^[a-zA-Z0-9]{8}$")
                    .When(x => x.IsUkBased)
                    .WithMessage("Please enter a valid Company registration number");

                RuleFor(x => x.CompanyRegistrationNumber)
                    .NotEmpty()
                    .WithMessage("Please enter a Company registration number")
                    .Matches("^[a-zA-Z0-9]{1,20}$")
                    .When(x => !x.IsUkBased)
                    .WithMessage("Please enter a valid Company registration number");
            });

            // Rules for ResponsibleActorsScheme
            When(x => x.ApplicationScheme == EApplicationScheme.ResponsibleActorsScheme, () => {
                RuleFor(x => x.CompanyName)
                    .NotEmpty()
                    .WithMessage("Please enter a Organisation name")
                    .MaximumLength(150)
                    .WithMessage("Organisation name cannot exceed 150 characters");

                RuleFor(x => x.CompanyRegistrationNumber)
                    .NotEmpty()
                    .WithMessage("Please enter a Organisation registration number")
                    .Length(4, 8)
                    .WithMessage("Organisation registration number must be between 4 and 8 characters");
            });
        }
    }
}