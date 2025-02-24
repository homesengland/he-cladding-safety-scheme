using FluentValidation;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.Leaseholder;

public class LeaseHolderEvidenceViewModelValidator : AbstractValidator<LeaseHolderEvidenceViewModel>
{
    private const int MaxSize = 20971520; // 20 * 1024 * 1024, 20mb

    public LeaseHolderEvidenceViewModelValidator()
    {
        When(x => x.SubmitAction == ESubmitAction.Upload, () =>
        {
            RuleFor(x => x.File)
                .NotEmpty()
                .WithMessage("File is required")
                .Must(x => x?.Length <= MaxSize)
                .WithMessage("File is larger than 20mb");
        });

        When(x => x.SubmitAction == ESubmitAction.Continue, () =>
            {
                RuleFor(x => x.AddedFiles)
                    .Must(x => x?.Any() == true)
                    .OverridePropertyName(nameof(LeaseHolderEvidenceViewModel.File))
                    .WithMessage("File is required");
            });
    }
}