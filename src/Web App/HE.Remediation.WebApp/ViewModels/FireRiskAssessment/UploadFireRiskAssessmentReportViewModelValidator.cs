
using FluentValidation;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAssessment;

public class UploadFireRiskAssessmentReportViewModelValidator : AbstractValidator<UploadFireRiskAssessmentReportViewModel>
{
    private const long FileSizeLimit = 100 * 1024 * 1024; // 100 MB

    public UploadFireRiskAssessmentReportViewModelValidator()
    {
        When(x => x.SubmitAction == ESubmitAction.Upload, () =>
        {
            RuleFor(x => x.FraReport)
                .NotNull()
                .WithMessage("Please upload a file")
                .Must(file => file is not null && file.Length <= FileSizeLimit)
                .WithMessage("File is larger than 100MB");
        });

        When(x => x.SubmitAction == ESubmitAction.Continue, () =>
        {
            RuleFor(x => x.FireRiskAssessmentType)
                .NotNull()
                .WithMessage("Please confirm the type of FRA.");

            RuleFor(x => x.AddedFra)
                .NotNull()
                .OverridePropertyName(x => x.FraReport)
                .WithMessage("Please upload a file");
        });
    }
}
