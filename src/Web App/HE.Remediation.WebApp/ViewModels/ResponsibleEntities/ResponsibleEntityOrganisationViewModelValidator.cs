using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities
{
    public class ResponsibleEntityOrganisationViewModelValidator : AbstractValidator<ResponsibleEntityOrganisationViewModel>
    {
        public ResponsibleEntityOrganisationViewModelValidator()
        {
            RuleFor(x => x.CompanyName)
                .NotEmpty()
                .WithMessage("Please enter a organisation name")
                .MaximumLength(150)
                .WithMessage("Company name cannot exceed 150 characters");

            RuleFor(x => x.RegistrationNumber)
                .NotEmpty()
                .WithMessage("Please enter a registration number")
                .Length(4,6)
                .WithMessage("Please enter a valid registration number between 4 and 6 characters.");
        }
    }
}