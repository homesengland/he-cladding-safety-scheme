using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAppraisal;

public class GetReportDetailsViewModelValidator : AbstractValidator<GetReportDetailsViewModel>
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
            .InclusiveBetween(1, 200)
            .WithMessage("Enter a number of storeys between 1 and 200");

        RuleFor(x => x.BuildingHeight)
            .NotEmpty()
            .WithMessage("Enter a height in metres")
            .InclusiveBetween(11, 1000)
            .WithMessage("Enter a height between 11 and 1000");

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
