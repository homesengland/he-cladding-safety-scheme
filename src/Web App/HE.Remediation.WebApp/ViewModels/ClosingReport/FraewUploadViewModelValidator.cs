using FluentValidation;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.ClosingReport;

public class FraewUploadViewModelValidator : AbstractValidator<UploadViewModel>
{
    private const int FileSizeLimit = 50 * 1024 * 1024; // 50mb
    public FraewUploadViewModelValidator()
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
                .OverridePropertyName(nameof(UploadViewModel.File))
                .WithMessage("File is required");

            RuleFor(x => x.ExitFraewDocumentType)
            .NotEmpty()
            .WithMessage("Type of FRA is required");
        });
    }
}
