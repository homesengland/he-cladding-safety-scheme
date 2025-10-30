using FluentValidation;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.ClosingReport
{
    public class UploadEvidenceSubmissionUploadViewModelValidator : AbstractValidator<UploadEvidenceSubmissionUploadViewModel>
    {
        private const int FileSizeLimit = 50 * 1024 * 1024; //50MB  
        public UploadEvidenceSubmissionUploadViewModelValidator()
        {
            When(x => x.SubmitAction == ESubmitAction.Upload, () =>
            {
                RuleFor(x => x.File)
                    .NotEmpty()
                    .WithMessage("Please upload supporting evidence for this contribution attempt.")
                    .Must(x => x?.Length <= FileSizeLimit)
                    .WithMessage("File is larger than 50MB");
            });

            When(x => x.SubmitAction == ESubmitAction.Continue, () =>
            {
                RuleFor(x => x.FileId)
                    .NotEmpty()
                    .OverridePropertyName(nameof(UploadViewModel.File))
                    .WithMessage("Please upload supporting evidence for this contribution attempt.");
            });
        }
    }
}
