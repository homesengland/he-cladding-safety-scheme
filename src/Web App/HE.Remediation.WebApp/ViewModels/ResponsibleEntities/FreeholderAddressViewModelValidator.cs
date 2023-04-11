using FluentValidation;
using HE.Remediation.WebApp.CustomPropertyValidators;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities
{
    public class FreeholderAddressViewModelValidator : AbstractValidator<FreeholderAddressViewModel>
    {
        public FreeholderAddressViewModelValidator()
        {
            RuleFor(x => x).SetValidator(new AddressViewModelValidator());
        }
    }
}
