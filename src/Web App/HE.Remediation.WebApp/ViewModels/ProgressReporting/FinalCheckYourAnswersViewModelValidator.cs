using FluentValidation;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class FinalCheckYourAnswersViewModelValidator : AbstractValidator<FinalCheckYourAnswersViewModel>
{
	public FinalCheckYourAnswersViewModelValidator()
	{
		When(x => x.OtherMembersAppointed != true, () =>
		{
			RuleFor(x => x.OtherMembersNeedsSupport)
				.NotNull()
				.WithMessage("Please confirm if you need support appointing team members.");

			RuleFor(x => x.OtherMembersNotAppointedReason)
				.NotEmpty()
				.WithMessage("Enter the reason why you have not appointed any team members.");
		});

		When(x => 
			x.TeamMembers?.Any(m => m.RoleId == (int)ETeamRole.ProjectManager) == true && 
			x.TeamMembers?.Any(m => m.RoleId == (int)ETeamRole.QuantitySurveyor) == true, 
			() =>
		{
			RuleFor(x => x.HasGco)
				.NotNull()
				.WithMessage("Please confirm if you have a grant certifying officer appointed.");
		});

		When(x => x.HasGco == true, () =>
		{
			RuleFor(x => x.TeamMember)
				.NotEmpty()
				.WithMessage("Select which member is your grant certifying officer.");

			RuleFor(x => x.HasGcoAddress)
				.Must(hasGcoAddress => hasGcoAddress)
				.WithMessage("Enter grant certifying officer's address");

			RuleFor(x => x.Signatory)
				.NotEmpty()
				.WithMessage("Enter authorised signatory");

			RuleFor(x => x.SignatoryEmailAddress)
				.NotEmpty()
				.WithMessage("Enter authorised signatory email address");

			RuleFor(x => x.DateAppointed)
				.NotNull()
				.WithMessage("Enter company's date of appointment");
		});

		When(x => x.QuotesSought == false, () =>
		{
			RuleFor(x => x.WhyYouHaveNotSoughtQuotes)
				.NotNull()
				.WithMessage("Enter why you have not sought quotes for the work");

			RuleFor(x => x.QuotesNotSoughtReason)
				.NotEmpty()
				.WithMessage("Enter additional information about why you have not sought quotes for the work");

			RuleFor(x => x.QuotesNeedsSupport)
				.NotNull()
				.WithMessage("Please confirm if you need support with quotes for the work.");
		});

		When(x => x.RequirePlanningPermission == EYesNoNonBoolean.Yes, () =>
		{
			RuleFor(x => x.AppliedForPlanningPermission)
				.NotNull()
				.WithMessage("Enter whether or not you have applied for planning permission");

			RuleFor(x => x.PlanningPermissionSubmittedDate)
				.NotNull()
				.When(x => x.AppliedForPlanningPermission == true)
				.WithMessage("Enter the date you submitted your application for planning permission");

			When(x => x.AppliedForPlanningPermission == false, () =>
			{
				RuleFor(x => x.PlanningPermissionPlannedSubmitDate)
					.NotNull()
					.WithMessage("Enter the date you plan to apply for your planning permission");

				RuleFor(x => x.ReasonPlanningPermissionNotApplied)
					.NotEmpty()
					.WithMessage("Enter the reason you have not applied for planning permission");

				RuleFor(x => x.PlanningPermissionNeedsSupport)
					.NotNull()
					.WithMessage("Please confirm if you need support with applying for planning permission");
			});
		});

		RuleFor(x => x.BuildingHasSafetyRegulatorRegistrationCode)
			.NotNull()
			.When(x => x.ShowBuildingSafetyRegulatorRegistration)
			.WithMessage("Enter if you have a building safety regulator registration code");

		RuleFor(x => x.BuildingSafetyRegulatorRegistrationCode)
			.NotEmpty()
			.When(x => x.BuildingHasSafetyRegulatorRegistrationCode == true && x.ShowBuildingSafetyRegulatorRegistration)
			.WithMessage("Enter your building safety regulator registration code");

		RuleFor(x => x.HasAppliedForBuildingControl)
			.NotNull()
			.WithMessage("Enter if you have applied for building control");

		RuleFor(x => x.SupportNeededReason)
			.NotEmpty()
			.When(x => x.QuotesNeedsSupport == true ||
			           x.OtherMembersNeedsSupport == true ||
			           x.PlanningPermissionNeedsSupport == true)
			.WithMessage("Enter a summary of your support needs");
	}
}