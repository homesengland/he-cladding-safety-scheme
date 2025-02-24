using FluentValidation;
namespace HE.Remediation.WebApp.ViewModels.Location;

public class PostCodeSelectionViewModelValidator: AbstractValidator<PostCodeSelectionViewModel>
{
    public PostCodeSelectionViewModelValidator()
    {        
        RuleFor(x => x.SelectedAddressId)
        .NotNull()
        .WithMessage("Please select an address")
        .Must(BeValidDropDownSelection)                
        .WithMessage("Please select an address");                
    }

    private bool BeValidDropDownSelection(string inputStr)
    {
        return ((inputStr != null) && (inputStr != "0"));
    }    
}
