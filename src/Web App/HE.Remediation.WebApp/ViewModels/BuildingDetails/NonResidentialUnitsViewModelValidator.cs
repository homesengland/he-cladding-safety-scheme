﻿using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.BuildingDetails
{
    public class NonResidentialUnitsViewModelValidator : AbstractValidator<NonResidentialUnitsViewModel>
    {
        public NonResidentialUnitsViewModelValidator()
        {
            RuleFor(x => x.NonResidentialUnitsCount)
                .NotEmpty()
                .WithMessage("Enter a number of non-residential units")
                .GreaterThan(0)
                .WithMessage("Enter a number of non-residential units")
                .LessThanOrEqualTo(999)
                .WithMessage("No more than 999 can be entered");
        }
    }
}
