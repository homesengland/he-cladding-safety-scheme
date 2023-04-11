using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAppraisal;

public class GetReportDetailsViewModelValidator: AbstractValidator<GetReportDetailsViewModel>
{
    public GetReportDetailsViewModelValidator()
    {
        RuleFor(x => x.AuthorsName)
            .NotEmpty()
            .WithMessage("Please enter a Authors name")
            .MaximumLength(150)
            .WithMessage("Authors name cannot exceed 150 characters");

        RuleFor(x => x.PeerReviewPerson)
            .NotEmpty()
            .WithMessage("Please enter a persons name")
            .MaximumLength(150)
            .WithMessage("The persons name cannot exceed 150 characters");

        RuleFor(x => x.UndertakingFirm)
            .NotEmpty()
            .WithMessage("Please enter a firm undertaking review")
            .MaximumLength(150)
            .WithMessage("Firm undertaking review cannot exceed 150 characters");

        RuleFor(x => x.NumberOfStoreys)
            .NotEmpty()
            .WithMessage("Enter a number of storeys")
            .GreaterThan(0)
            .WithMessage("Enter a number of storeys")
            .LessThanOrEqualTo(999)
            .WithMessage("No more than 999 can be entered");
        
        RuleFor(x => x.BuildingHeight)
            .NotEmpty()
            .WithMessage("Enter a height in meters")
            .GreaterThanOrEqualTo(11)
            .LessThanOrEqualTo(18)                
            .WithMessage("Please enter a height between 11 and 18 metres");

         RuleFor(x => x.BasicComplexId)
                .NotEmpty()
                .WithMessage("Please select an option");
    }
}
