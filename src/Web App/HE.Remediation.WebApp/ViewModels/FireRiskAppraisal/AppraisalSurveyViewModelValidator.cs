using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAppraisal
{
    public class AppraisalSurveyViewModelValidator : AbstractValidator<AppraisalSurveyDetailsViewModel>
    {
        public AppraisalSurveyViewModelValidator()
        {
            RuleFor(x => x.FireRiskAssessorId)
                .NotNull()
                .GreaterThan(0)
                .When(x => !x.FireAccessorNotOnPanel)
                .WithMessage("Please select a company");


            RuleFor(x => x.DateOfInstruction)
                .NotNull()
                .WithMessage("Enter a valid instruction date in a DD MM YYYY format")
                .Must(BeAValidDate)
                .WithMessage("Enter a valid instruction date in a DD MM YYYY format");

            RuleFor(x => x.SurveyDate)
                .NotNull()
                .WithMessage("Enter a valid instruction date in a DD MM YYYY format")
                .Must(BeAValidDate)
                .WithMessage("Enter a valid instruction date in a DD MM YYYY format");
        }

        private bool BeAValidDate(DateTime? date)
        {
            return date >= new DateTime(2022, 01, 01);
        }
    }
}