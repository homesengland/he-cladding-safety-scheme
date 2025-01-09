using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class UploadEvidenceViewModelValidator : AbstractValidator<UploadEvidenceViewModel>
{
    private const long FileSizeLimit = 50 * 1024 * 1024; // 50mb

    public UploadEvidenceViewModelValidator()
    {
        When(x => x.File is not null, () =>
        {
            RuleFor(x => x.File)
                .NotEmpty()
                .WithMessage("File upload required")
                .Must(x => x?.Length <= FileSizeLimit)
                .WithMessage("File is larger than 50MB");
        });
    }
}