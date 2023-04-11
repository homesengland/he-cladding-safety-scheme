using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities
{
    public class ResponsibleEntityCompanyAddressViewModelValidator : AbstractValidator<ResponsibleEntityCompanyAddressViewModel>
    {
        public ResponsibleEntityCompanyAddressViewModelValidator()
        {
            RuleFor(x => x).SetValidator(new AddressViewModelValidator());
        }
    }
}