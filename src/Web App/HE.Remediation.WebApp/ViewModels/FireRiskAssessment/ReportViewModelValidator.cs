using FluentValidation;

using HE.Remediation.Core.Enums;
using HE.Remediation.WebApp.CustomPropertyValidators;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAssessment;

public class ReportViewModelValidator : AbstractValidator<ReportViewModel>
{
    public ReportViewModelValidator()
    {
        RuleFor(x => x.AssessorId)
            .NotNull()
            .WithMessage("Select an option");

        RuleFor(x => x.FraDate)
            .NotNull()
            .WithMessage("Enter a date")
            .LessThanOrEqualTo(DateTime.Today)
            .WithMessage("Date entered must be in the past")
            .ValidDate();

        RuleFor(x => x.FraCommissionerType)
            .NotNull()
            .WithMessage("Select who commissioned the FRA")
            .When(x => x.ApplicationScheme == EApplicationScheme.ResponsibleActorsScheme);
    }
}