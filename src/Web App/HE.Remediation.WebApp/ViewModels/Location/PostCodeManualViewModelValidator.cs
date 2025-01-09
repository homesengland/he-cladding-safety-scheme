using FluentValidation;
using HE.Remediation.Core.Enums;
using HE.Remediation.WebApp.CustomPropertyValidators;

namespace HE.Remediation.WebApp.ViewModels.Location;

public class PostCodeManualViewModelValidator: AbstractValidator<PostCodeManualViewModel>
{
    private const int UKDropDownCountryValue = 1;

    public PostCodeManualViewModelValidator(bool CheckLocalAuthority, bool CheckCountry)
    {
        RuleFor(x => x.NameNumber)
        .NotNull()
        .WithMessage("Please enter a Building name")
        .MaximumLength(150)
        .WithMessage("Building name or number cannot exceed 150 characters");

        RuleFor(x => x.AddressLine1)
            .NotNull()
            .WithMessage("Please enter a 1st line of address")
            .MaximumLength(150)
            .WithMessage("1st line of address cannot exceed 150 characters");

        RuleFor(x => x.AddressLine2)
            .MaximumLength(150)
            .WithMessage("2nd line of address cannot exceed 150 characters");

        RuleFor(x => x.City)
            .NotNull()
            .WithMessage("Please enter a town or city")
            .MaximumLength(150)
            .WithMessage("Town or city cannot exceed 150 characters");

        if (CheckLocalAuthority)
        {
            RuleFor(x => x.LocalAuthority)
            .NotNull()
            .WithMessage("Local Authority must be entered")
            .MaximumLength(150)
            .WithMessage("Local Authority cannot exceed 150 characters");
        }

        RuleFor(x => x.County)
            .MaximumLength(150)
            .WithMessage("County cannot exceed 150 characters");

        if (CheckCountry)
        {            
            RuleFor(x => x.CountryId)
                            .NotNull()
                            .WithMessage("Please select a country")
                            .Must(BeValidCountryDropDownSelection)                
                            .WithMessage("Please select a country");

            RuleFor(x => x.Postcode)
                    .NotEmpty()
                    .WithMessage("Please enter a postcode");                    

            // When Uk is selected, then also check the post code
            When(x => x.CountryId == UKDropDownCountryValue,   () =>
            {
                RuleFor(x => x.Postcode)
                    .ValidGovPostcode();                                       
            });            
        }
        else
        {
            RuleFor(x => x.Postcode)
            .NotEmpty()
            .WithMessage("Please enter a postcode")
            .ValidGovPostcode();                   
        }
    }

    private bool BeValidCountryDropDownSelection(int? inputStr)
    {
        return ((inputStr != null) && (inputStr != 0));
    }
}
