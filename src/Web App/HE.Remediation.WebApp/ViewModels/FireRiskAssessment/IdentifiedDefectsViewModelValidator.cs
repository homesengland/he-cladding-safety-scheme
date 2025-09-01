using FluentValidation;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAssessment;

public class IdentifiedDefectsViewModelValidator : AbstractValidator<IdentifiedDefectsViewModel>
{
    public IdentifiedDefectsViewModelValidator()
    {
        RuleFor(x => x.InternalFireSafetyDefects)
            .NotEmpty()
            .WithMessage("Select at least one option");

        When(x => x.InternalFireSafetyDefects.Contains(EInternalFireSafetyDefect.Other), () =>
        {
            RuleFor(x => x.OtherInternalDefect)
                .NotEmpty()
                .WithMessage("Enter 'other' description")
                .MaximumLength(150)
                .WithMessage("'Other' description cannot be more than 150 characters");
        });
    }
}