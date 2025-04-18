﻿using FluentValidation;

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
                .Matches(@"^[0-9]{6,8}$")
                .WithMessage("Must be between 6 and 8 digits long");

            RuleFor(x => x.SortCode)
                .NotEmpty()
                .WithMessage("Please enter a valid sort code")
                .Matches(@"^[0-9]{6}$")
                .WithMessage("Must be 6 digits long");

            RuleFor(x => x.VatNumber)
                .Matches(@"^[A-Za-z]{2}[0-9]{9}$")
                .WithMessage("Please enter a valid VAT number");
        }
    }
}