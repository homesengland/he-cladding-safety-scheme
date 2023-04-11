using FluentValidation;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAppraisal;

public class UploadFireRiskAppraisalReportViewModelValidator : AbstractValidator<UploadFireRiskAppraisalReportViewModel>
{
    private const long FileSizeLimit = 50 * 1024 * 1024; // 50mb

    public UploadFireRiskAppraisalReportViewModelValidator()
    {
        When(x => x.Fraew is not null, () =>
        {
            RuleFor(x => x.Fraew)
                .NotEmpty()
                .WithMessage("File upload required")
                .Must(x => x?.Length <= FileSizeLimit)
                .WithMessage("File is larger than 50MB");
        });

        When(x => x.FraewSummary is not null, () =>
        {
            RuleFor(x => x.FraewSummary)
                .NotEmpty()
                .WithMessage("File upload required")
                .Must(x => x?.Length <= FileSizeLimit)
                .WithMessage("File is larger than 50MB");
        });
    }
}