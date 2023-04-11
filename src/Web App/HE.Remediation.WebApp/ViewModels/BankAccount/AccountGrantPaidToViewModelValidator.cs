using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.BankAccount
{
    public class AccountGrantPaidToViewModelValidator : AbstractValidator<AccountGrantPaidToViewModel>
    {
        public AccountGrantPaidToViewModelValidator()
        {
            RuleFor(x => x.BankDetailsRelationship)
                .NotEmpty()
                .WithMessage("Select an option");
        }
    }
}