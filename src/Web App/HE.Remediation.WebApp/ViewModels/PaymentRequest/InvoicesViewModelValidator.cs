using FluentValidation;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.PaymentRequest;

public class InvoicesViewModelValidator : AbstractValidator<InvoicesViewModel>
{
    private const int Mb = 100;
    private const int FileSizeLimit = Mb * 1024 * 1024; // 100mb

    public InvoicesViewModelValidator()
    {
        When(x => x.SubmitAction == ESubmitAction.Upload, () =>
        {
            RuleFor(x => x.File)
                .NotEmpty()
                .WithMessage("You must upload at least one file")
                .Must(x => x?.Length <= FileSizeLimit)
                .WithMessage($"File is larger than {Mb}mb");
        });

        When(x => x.SubmitAction == ESubmitAction.Continue, () =>
        {
            RuleFor(x => x.AddedFiles)
                .NotEmpty()
                .OverridePropertyName(nameof(InvoicesViewModel.File))
                .WithMessage("You must upload at least one file");
        });

    }
}