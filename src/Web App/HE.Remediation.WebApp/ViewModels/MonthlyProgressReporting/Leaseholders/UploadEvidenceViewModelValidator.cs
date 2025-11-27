using FluentValidation;
using HE.Remediation.Core.Enums;
using HE.Remediation.WebApp.ViewModels.VariationRequest;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.Leaseholders;

public class UploadEvidenceViewModelValidator : AbstractValidator<UploadEvidenceViewModel>
{
    private const int FileSizeLimit = 50 * 1024 * 1024; // 50mb

    public UploadEvidenceViewModelValidator()
    {
        When(x => x.SubmitAction == ESubmitAction.Upload, () =>
        {
            RuleFor(x => x.File)
                .NotEmpty()
                .WithMessage("File is required")
                .Must(x => x?.Length <= FileSizeLimit)
                .WithMessage("File is larger than 50MB");
        });

        When(x => x.SubmitAction == ESubmitAction.Continue, () =>
        {
            RuleFor(x => x.AddedFiles)
                .Must(x => x?.Any() == true)
                .OverridePropertyName(nameof(EvidenceViewModel.File))
                .WithMessage("File is required");
        });
    }
}