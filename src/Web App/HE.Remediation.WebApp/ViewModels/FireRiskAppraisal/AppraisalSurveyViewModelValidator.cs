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
                .WithMessage("Enter a valid instruction date in a DD MM YYYY format")
                .Must(NotInTheFuture)
                .WithMessage("The instruction date must not be in the future");

            RuleFor(x => x.SurveyDate)
                .NotNull()
                .WithMessage("Enter a valid survey date in a DD MM YYYY format")
                .Must(BeAValidDate)
                .WithMessage("Enter a valid survey date in a DD MM YYYY format")
                .GreaterThan(x => x.DateOfInstruction)
                .WithMessage("The Survey date must be after the instruction date")
                .Must(NotInTheFuture)
                .WithMessage("The Survey date must not be in the future");                
        }        

        private bool BeAValidDate(DateTime? date)
        {
            return date >= new DateTime(2020, 01, 01);
        }

        private bool NotInTheFuture(DateTime? date)
        {
            return date.HasValue ? date.Value.Date <= DateTime.Now.Date : true;
        }
    }
}