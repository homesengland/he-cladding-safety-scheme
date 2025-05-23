﻿using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class AddRoleViewModelValidator : AbstractValidator<AddRoleViewModel> 
{
    public AddRoleViewModelValidator()
    {
        RuleFor(x => x.TeamRole)
            .NotNull()
            .WithMessage("Please select a role you wish to add");
    }
}
