﻿using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.BuildingDetails;

public class BuildingHasSafetyRegulatorRegistrationCodeViewModelValidator : AbstractValidator<BuildingHasSafetyRegulatorRegistrationCodeViewModel>
{
    public BuildingHasSafetyRegulatorRegistrationCodeViewModelValidator()
    {
        RuleFor(x => x.BuildingHasSafetyRegulatorRegistrationCode)
            .NotNull()
            .WithMessage("Select yes if you have a Building Safety Regulator registration code");
    }
}