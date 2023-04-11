﻿using FluentValidation;

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
                .WithMessage("Please enter a Company registration number")
                .MaximumLength(150)
                .WithMessage("Company registration cannot exceed 150 characters");
        }
    }
}
