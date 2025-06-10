
using FluentValidation;

using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAppraisal;

public class UploadFireRiskAssessmentReportViewModelValidator : AbstractValidator<UploadFireRiskAssessmentReportViewModel>
{
    private const long FileSizeLimit = 100 * 1024 * 1024; // 100 MB

    public UploadFireRiskAssessmentReportViewModelValidator()
    {
        // Validate file size only if a file is selected
        When(x => x.FraReport is not null, () =>
        {
            RuleFor(x => x.FraReport)
                .Must(file => file.Length <= FileSizeLimit)
                .WithMessage("File is larger than 100MB");
        });

        // Validate radio button only when SubmitAction is Continue
        When(x => x.AddedFra is not null && x.SubmitAction == ESubmitAction.Continue, () =>
        {
            RuleFor(x => x.FireRiskAssessmentType)
                .NotNull()
                .WithMessage("Please confirm the type of FRA.");
        });
    }
}
