using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.Shared
{
    public class FileUploadViewModelValidator : AbstractValidator<FileUploadViewModel>
    {
        public FileUploadViewModelValidator()
        {
            RuleFor(x => x.File)
                .NotNull();
        }
    }
}
