using FluentValidation.Results;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.KeyDates;
public class KeyDatesViewModel
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }

    // Works Planning
    public bool IsWorksPlanning => ExpectedTenderDate.HasValue && ExpectedLeadContractorAppointmentDate.HasValue;
    public DateTime? ExpectedTenderDate { get; set; }
    public DateTime? ExpectedLeadContractorAppointmentDate { get; set; }
    public DateTime? ActualTenderDate { get; set; }
    public DateTime? ActualLeadContractAppointmentDate { get; set; }
    public DateTime? ExpectedWorksPackageSubmissionDate { get; set; }
    public string WorksPlanningChangeType { get; set; }
    public string WorksPlanningChangeReason { get; set; }

    public EContractorTenderType? ContractorTenderTypeId { get; set; }
    public string ContractorTenderReason { get; set; }

    public DateTime? PreviousExpectedTenderDate { get; set; }
    public DateTime? PreviousExpectedLeadContractorAppointmentDate { get; set; }
    public DateTime? PreviousActualTenderDate { get; set; }
    public DateTime? PreviousActualLeadContractAppointmentDate { get; set; }
    public DateTime? PreviousExpectedWorksPackageSubmissionDate { get; set; }
    public bool WorksPlanningDatesHaveChanged => ((PreviousExpectedTenderDate.HasValue && ExpectedTenderDate.HasValue && ExpectedTenderDate != PreviousExpectedTenderDate)
                    || (PreviousExpectedLeadContractorAppointmentDate.HasValue && ExpectedLeadContractorAppointmentDate.HasValue && ExpectedLeadContractorAppointmentDate != PreviousExpectedLeadContractorAppointmentDate)
                    || (PreviousActualTenderDate.HasValue && ActualTenderDate.HasValue && ActualTenderDate != PreviousActualTenderDate)
                    || (PreviousActualLeadContractAppointmentDate.HasValue && ActualLeadContractAppointmentDate.HasValue && ActualLeadContractAppointmentDate != PreviousActualLeadContractAppointmentDate)
                    || (PreviousExpectedWorksPackageSubmissionDate.HasValue && ExpectedWorksPackageSubmissionDate.HasValue && ExpectedWorksPackageSubmissionDate != PreviousExpectedWorksPackageSubmissionDate));

    // Building Control
    public bool IsBuildingControl { get { return BuildingControlExpectedApplicationDate != null || BuildingControlActualApplicationDate != null || BuildingControlDecisionDate != null; } }
    public DateTime? BuildingControlExpectedApplicationDate { get; set; }
    public DateTime? BuildingControlActualApplicationDate { get; set; }
    public string BuildingControlGateway2Reference { get; set; }
    public DateTime? BuildingControlValidationDate { get; set; }
    public DateTime? BuildingControlDecisionDate { get; set; }
    public EBuildingControlDecisionType? BuildingControlDecision { get; set; }
    public IList<string> BuildingControlDecisionFiles { get; set; } = new List<string>();
    public string BuildingControlChangeType { get; set; }
    public string BuildingControlChangeReason { get; set; }

    public DateTime? PreviousBuildingControlExpectedApplicationDate { get; set; }
    public DateTime? PreviousBuildingControlActualApplicationDate { get; set; }
    public DateTime? PreviousBuildingControlValidationDate { get; set; }
    public DateTime? PreviousBuildingControlDecisionDate { get; set; }
    public bool BuildingControlDatesHaveChanged => ((PreviousBuildingControlExpectedApplicationDate.HasValue && BuildingControlExpectedApplicationDate.HasValue && BuildingControlExpectedApplicationDate != PreviousBuildingControlExpectedApplicationDate)
                                                    || (PreviousBuildingControlActualApplicationDate.HasValue && BuildingControlActualApplicationDate.HasValue && BuildingControlActualApplicationDate != PreviousBuildingControlActualApplicationDate)
                                                    || (PreviousBuildingControlValidationDate.HasValue && BuildingControlValidationDate.HasValue && BuildingControlValidationDate != PreviousBuildingControlValidationDate)
                                                    || (PreviousBuildingControlDecisionDate.HasValue && BuildingControlDecisionDate.HasValue && BuildingControlDecisionDate != PreviousBuildingControlDecisionDate));

    // Planning Permission
    public bool IsPlanningPermission { get { return WorksNeedPlanningPermission != null; } }
    public int? WorksNeedPlanningPermission { get; set; }
    public string WorksNeedPlanningPermissionParsed { 
        get {
            if (WorksNeedPlanningPermission == 0) return "No";
            if (WorksNeedPlanningPermission == 1) return "Yes";
            if (WorksNeedPlanningPermission == 2) return "Don't Know";
            return null;
        } 
    }
    public bool? HaveAppliedPlanningPermission { get; set; }
    public DateTime? PlanningPermissionDateSubmitted { get; set; }
    public DateTime? PlanningPermissionDateApproved { get; set; }
    public string PlanningPermissionReasonNotApplied { get; set; }
    public DateTime? PlanningPermissionPlanToSubmitDate { get; set; }
    public string PlanningPermissionChangeType { get; set; }
    public string PlanningPermissionChangeReason { get; set; }

    public DateTime? PreviousPlanningPermissionDateSubmitted { get; set; }
    public DateTime? PreviousPlanningPermissionDateApproved { get; set; }
    public DateTime? PreviousPlanningPermissionPlanToSubmitDate { get; set; }
    public bool PlanningPermissionDatesHaveChanged => ((PreviousPlanningPermissionDateSubmitted.HasValue && PlanningPermissionDateSubmitted.HasValue && PlanningPermissionDateSubmitted != PreviousPlanningPermissionDateSubmitted)
                                                       || (PreviousPlanningPermissionDateApproved.HasValue && PlanningPermissionDateApproved.HasValue && PlanningPermissionDateApproved != PreviousPlanningPermissionDateApproved)
                                                       || (PreviousPlanningPermissionPlanToSubmitDate.HasValue && PlanningPermissionPlanToSubmitDate.HasValue && PlanningPermissionPlanToSubmitDate != PreviousPlanningPermissionPlanToSubmitDate));

    // Remediation
    public bool IsRemediation { get { return RemediationFullCompletionOfWorksDate != null; } }
    public DateTime? RemediationFullCompletionOfWorksDate { get; set; }
    public DateTime? RemediationPracticalCompletionDate { get; set; }
    public string RemediationChangeType { get; set; }
    public string RemediationChangeReason { get; set; }

    public DateTime? PreviousRemediationFullCompletionOfWorksDate { get; set; }
    public DateTime? PreviousRemediationPracticalCompletionDate { get; set; }
    public bool RemediationDatesHaveChanged => ((PreviousRemediationFullCompletionOfWorksDate.HasValue && RemediationFullCompletionOfWorksDate.HasValue && RemediationFullCompletionOfWorksDate != PreviousRemediationFullCompletionOfWorksDate)
                                                || (PreviousRemediationPracticalCompletionDate.HasValue && RemediationPracticalCompletionDate.HasValue && RemediationPracticalCompletionDate != PreviousRemediationPracticalCompletionDate));

    public bool RemediationDatesComeLast()
    {
        var earliestRemediationDate = new[] { RemediationFullCompletionOfWorksDate, RemediationPracticalCompletionDate }.Where(d => d.HasValue).Min();

        if (earliestRemediationDate == null) return true;

        var otherDates = new List<DateTime?>
        {
            //wp
            ExpectedTenderDate,
            ExpectedLeadContractorAppointmentDate,
            ActualTenderDate,
            ActualLeadContractAppointmentDate,
            ExpectedWorksPackageSubmissionDate,
            //bc
            BuildingControlExpectedApplicationDate,
            BuildingControlActualApplicationDate,
            BuildingControlValidationDate,
            BuildingControlDecisionDate,
            //pp
            PlanningPermissionDateSubmitted,
            PlanningPermissionDateApproved,
            PlanningPermissionPlanToSubmitDate,
        };

        var latestOtherDate = otherDates.Where(d => d.HasValue).Max();

        return latestOtherDate == null || earliestRemediationDate >= latestOtherDate;
    }
}