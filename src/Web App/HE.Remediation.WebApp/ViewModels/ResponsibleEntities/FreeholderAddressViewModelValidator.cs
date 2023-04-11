using FluentValidation;

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
