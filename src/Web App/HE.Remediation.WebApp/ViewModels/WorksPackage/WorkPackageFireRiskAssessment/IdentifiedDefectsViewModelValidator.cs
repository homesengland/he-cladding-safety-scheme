using FluentValidation;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageFireRiskAssessment;

public class IdentifiedDefectsViewModelValidator : AbstractValidator<IdentifiedDefectsViewModel>
{
    public IdentifiedDefectsViewModelValidator()
    {
        RuleFor(x => x.InternalFireSafetyDefects)
            .NotEmpty()
            .WithMessage("Select at least one option");

        RuleFor(x => x.OtherInternalDefect)
            .NotEmpty()
            .When(x => x.InternalFireSafetyDefects is not null && x.InternalFireSafetyDefects.Contains(EInternalFireSafetyDefect.Other))
            .WithMessage("Enter 'other' defect");
    }
}