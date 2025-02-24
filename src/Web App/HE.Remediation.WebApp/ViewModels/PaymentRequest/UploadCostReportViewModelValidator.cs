using FluentValidation;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.PaymentRequest;

public class UploadCostReportViewModelValidator : AbstractValidator<UploadCostReportViewModel>
{
    private const long FileSizeLimit = 20 * 1024 * 1024; // 20mb

    public UploadCostReportViewModelValidator()
    {
        When(x => x.SubmitAction == ESubmitAction.Upload, () =>
        {
            RuleFor(x => x.File)
                .NotEmpty()
                .WithMessage("You must upload at least one file")
                .Must(x => x?.Length <= FileSizeLimit)
                .WithMessage("File is larger than 20mb");
        });

        When(x => x.SubmitAction == ESubmitAction.Continue, () =>
        {
            RuleFor(x => x.AddedFiles)
                .Must(x => x?.Any() == true)
                .OverridePropertyName(nameof(UploadCostReportViewModel.File))
                .WithMessage("You must upload at least one file");
        });        
    }
}
