using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAppraisal;

public class GetReportDetailsViewModelValidator: AbstractValidator<GetReportDetailsViewModel>
{
    public GetReportDetailsViewModelValidator()
    {
        RuleFor(x => x.AuthorsName)
            .NotEmpty()
            .WithMessage("Please enter name of author of report")
            .MaximumLength(150)
            .WithMessage("Authors name cannot exceed 150 characters");

        RuleFor(x => x.PeerReviewPerson)
            .NotEmpty()
            .WithMessage("Please enter a persons name")
            .MaximumLength(150)
            .WithMessage("The persons name cannot exceed 150 characters");

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
            .WithMessage("The height of the building must be 11m or more");

         RuleFor(x => x.BasicComplexId)
                .NotEmpty()
                .WithMessage("Select an option");

         RuleFor(x => x.FraewCost)
             .NotEmpty()
             .WithMessage("Enter the cost of FRAEW report")
             .GreaterThanOrEqualTo(0)
             .WithMessage("Enter a positive number");
    }
}
