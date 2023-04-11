using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.BankAccount
{
    public class BankAccountDetailsViewModelValidator : AbstractValidator<BankAccountDetailsViewModel>
    {
        public BankAccountDetailsViewModelValidator()
        {
            RuleFor(x => x.NameOnTheAccount)
                .NotEmpty()
                .WithMessage("Enter name on the account");

            RuleFor(x => x.BankName)
                .NotEmpty()
                .WithMessage("Please enter a bank name");

            RuleFor(x => x.BranchName)
                .NotEmpty()
                .WithMessage("Please enter a branch name");

            RuleFor(x => x.AccountNumber)
                .NotEmpty()
                .WithMessage("Please enter a valid account number")
                .Must(x => x.ToString().Length >= 6 && x.ToString().Length <= 8)
                .WithMessage("Must be between 6 and 8 digits long");

            RuleFor(x => x.SortCode)
                .NotEmpty()
                .WithMessage("Please enter a valid sort code")
                .Must(x => x.ToString().Length == 6)
                .WithMessage("Must be 6 digits long");
        }
    }
}