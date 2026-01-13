using FluentValidation;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.KeyDates;
public class KeyDatesViewModelValidator : AbstractValidator<KeyDatesViewModel>
{
    private const string WorksPlanning = nameof(WorksPlanning);
    private const string BuildingControl = nameof(BuildingControl);
    private const string PlanningPermission = nameof(PlanningPermission);
    private const string Remediation = nameof(Remediation);

    public KeyDatesViewModelValidator()
    {
        RuleFor(x => x)
            // WORKS PLANNING
            .Custom((model, context) =>
            {
                if (!model.ExpectedTenderDate.HasValue && !model.ActualTenderDate.HasValue)
                {
                    context.AddFailure(WorksPlanning, "Please enter either an expected and/or actual Date of Tender");
                }
                if (!model.ExpectedLeadContractorAppointmentDate.HasValue && !model.ActualLeadContractAppointmentDate.HasValue)
                {
                    context.AddFailure(WorksPlanning, "Please enter either an expected and/or actual Lead Contractor Appointment Date");
                }

                if (model.WorksPlanningDatesHaveChanged && string.IsNullOrWhiteSpace(model.WorksPlanningChangeType))
                {
                    context.AddFailure(WorksPlanning, "Please enter why works planning dates have slipped");
                }

                if (model.WorksPlanningDatesHaveChanged && string.IsNullOrWhiteSpace(model.WorksPlanningChangeReason))
                {
                    context.AddFailure(WorksPlanning, "Please enter additional details about why works planning dates have slipped");
                }

                if (!model.ContractorTenderTypeId.HasValue)
                {
                    context.AddFailure(WorksPlanning, "Please enter how you are obtaining your contractor");
                }

                if ((model.ContractorTenderTypeId is EContractorTenderType.NonCompetitive or EContractorTenderType.UsingOriginalContractor) 
                    && string.IsNullOrWhiteSpace(model.ContractorTenderReason))
                {
                    context.AddFailure(WorksPlanning, "Please enter the reason you are not running a competitive tender to obtain your contractor");
                }
                
            })
            // BUILDING CONTROL
            .Custom((model, context) =>
            {
                if (!model.BuildingControlExpectedApplicationDate.HasValue && !model.BuildingControlActualApplicationDate.HasValue)
                {
                    context.AddFailure(BuildingControl, "Please enter either an expected and/or actual Building Control Expected Application Date");
                }

                if (model.BuildingControlDecisionDate.HasValue && !model.BuildingControlDecision.HasValue)
                {
                    context.AddFailure(BuildingControl, "Provide a Building Control Decision");
                }

                if (model is
                    {
                        BuildingControlDecision: EBuildingControlDecisionType.ApproveWithRecommendations or EBuildingControlDecisionType.Rejected,
                        BuildingControlDecisionFiles: not { Count: > 0 }
                    })
                {
                    context.AddFailure(BuildingControl, "Provide at least 1 document to support the building control decision");
                }

                if (model.BuildingControlDatesHaveChanged && string.IsNullOrWhiteSpace(model.BuildingControlChangeType))
                {
                    context.AddFailure(BuildingControl, "Please enter a reason why building control dates have slipped");
                }

                if (model.BuildingControlDatesHaveChanged && string.IsNullOrWhiteSpace(model.BuildingControlChangeReason))
                {
                    context.AddFailure(BuildingControl, "Please enter additional details about why building control dates have slipped");
                }

            })            
            // PLANNING PERMISSION
            .Custom((model, context) =>
            {
                var haveAppliedPlanningPermission = model.HaveAppliedPlanningPermission ?? false;
                if (model.WorksNeedPlanningPermission == null)
                {
                    context.AddFailure(PlanningPermission, "Please enter indicate if planning permission is required");
                }
                if (model.WorksNeedPlanningPermission == 1 && haveAppliedPlanningPermission && !model.PlanningPermissionDateSubmitted.HasValue)
                {
                    context.AddFailure(PlanningPermission, "Please enter a Submission Date");
                }
                if (model.WorksNeedPlanningPermission == 1 && !haveAppliedPlanningPermission && model.PlanningPermissionReasonNotApplied == null)
                {
                    context.AddFailure(PlanningPermission, "Please enter a reason why planning permission is not required");
                }
                if (model.WorksNeedPlanningPermission == 1 && !haveAppliedPlanningPermission && model.PlanningPermissionPlanToSubmitDate == null)
                {
                    context.AddFailure(PlanningPermission, "Please enter a date you plan to submit for planning permission");
                }

                if (model.PlanningPermissionDatesHaveChanged && string.IsNullOrWhiteSpace(model.PlanningPermissionChangeType))
                {
                    context.AddFailure(PlanningPermission, "Please enter a reason why planning permission dates have slipped");
                }

                if (model.PlanningPermissionDatesHaveChanged && string.IsNullOrWhiteSpace(model.PlanningPermissionChangeReason))
                {
                    context.AddFailure(PlanningPermission, "Please enter additional details about why planning permission dates have slipped");
                }

            })
            // REMEDIATION
            .Custom((model, context) =>
            {
                if (!model.RemediationFullCompletionOfWorksDate.HasValue)
                {
                    context.AddFailure(Remediation, "Please enter an expected remediation start date");
                }
                if (!model.RemediationPracticalCompletionDate.HasValue)
                {
                    context.AddFailure(Remediation, "Please enter an expected practical completion date");
                }

                if (model.RemediationDatesHaveChanged && string.IsNullOrWhiteSpace(model.RemediationChangeType))
                {
                    context.AddFailure(Remediation, "Please enter a reason why remediation dates have slipped");
                }

                if (model.RemediationDatesHaveChanged && string.IsNullOrWhiteSpace(model.RemediationChangeReason))
                {
                    context.AddFailure(Remediation, "Please enter additional details about why remediation dates have slipped");
                }

                if(!model.RemediationDatesComeLast())
                {
                    context.AddFailure(Remediation, "Remediation dates must be after all dates provided in other Key Dates sections.");
                }

            });
    }
}