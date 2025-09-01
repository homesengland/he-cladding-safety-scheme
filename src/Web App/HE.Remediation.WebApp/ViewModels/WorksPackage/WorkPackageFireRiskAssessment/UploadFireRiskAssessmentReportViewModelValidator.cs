using FluentValidation;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageFireRiskAssessment;

public class UploadFireRiskAssessmentReportViewModelValidator : AbstractValidator<UploadFireRiskAssessmentReportViewModel>
{
    public UploadFireRiskAssessmentReportViewModelValidator()
    {
        RuleFor(x => x.File)
            .NotNull()
            .When(x => x.AddedFiles is null || x.AddedFiles.Count == 0)
            .WithMessage("Select a file");

        RuleFor(x => x.AddedFiles)
            .NotEmpty()
            .When(x => x.File is null)
            .OverridePropertyName(x => x.File)
            .WithMessage("Select a file");

        RuleFor(x => x.FireRiskAssessmentType)
            .NotNull()
            .WithMessage("Select an option");
    }
}