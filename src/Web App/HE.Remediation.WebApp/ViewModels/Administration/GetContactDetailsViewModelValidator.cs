using FluentValidation;
using HE.Remediation.WebApp.CustomPropertyValidators;

namespace HE.Remediation.Core.UseCase.Areas.Administration.ContactDetails.GetContactDetails
{
    public class GetContactDetailsViewModelValidator : AbstractValidator<GetContactDetailsRequest>
    {
        public GetContactDetailsViewModelValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("Please enter a First name");
            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("Please enter a Last name");
            RuleFor(x => x.ContactNumber)
                .NotEmpty()
                .WithMessage("Please enter a Contact number")
                .NotValidTelephoneGB()
                .WithMessage("Enter a Phone number, like 01632960001, 07700900982"); ;
        }
    }
}
