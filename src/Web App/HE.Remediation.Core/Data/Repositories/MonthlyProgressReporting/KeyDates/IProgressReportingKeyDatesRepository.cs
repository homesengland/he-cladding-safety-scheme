using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.KeyDates;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.KeyDates.BuildingControl;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.KeyDates.PlanningPermission;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.KeyDates.Remediation;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.KeyDates.WorksPlanning;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.ProjectSupport;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Data.StoredProcedureResults.MonthlyProgressReport;
using HE.Remediation.Core.Data.StoredProcedureResults.MonthlyProgressReport.KeyDates;
using HE.Remediation.Core.Data.StoredProcedureResults.MonthlyProgressReport.KeyDates.BuildingControl;
using HE.Remediation.Core.Data.StoredProcedureResults.MonthlyProgressReport.KeyDates.PlanningPermission;
using HE.Remediation.Core.Data.StoredProcedureResults.MonthlyProgressReport.KeyDates.Remediation;
using HE.Remediation.Core.Data.StoredProcedureResults.MonthlyProgressReport.KeyDates.WorksPlanning;

namespace HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting.KeyDates;

public interface IProgressReportingKeyDatesRepository
{
    Task<ProgressReportKeyDatesResult> GetProgressReportKeyDates(Guid monthlyProgressReportId);
    Task UpdateKeyDatesStatus(SetKeyDatesStatusParameters parameters);
    Task<GetProgressReportKeyDatesChangeFlagsResult> GetProgressReportKeyDatesChangeFlags(GetMonthlyProgressReportParameters parameters);

    // Works Planning

    Task SetProgressReportWorksPlanningKeyDates(SetProgressReportWorksPlanningKeyDatesParameters parameters);
    Task<IReadOnlyCollection<GetProgressReportKeyDatesChangeTypesResult>> GetProgressReportKeyDatesChangeTypes();
    Task<GetWorksPlanningDatesChangedResult> GetWorksPlanningDatesChanged(GetWorksPlanningDatesChangedParameters parameters);
    Task SetWorksPlanningDatesChanged(SetWorksPlanningDatesChangedParameters parameters);

    Task<GetContractorTenderDetailsResult> GetContractorTenderDetails(GetMonthlyProgressReportParameters parameters);
    Task SetContractorTenderType(SetContractorTenderTypeParameters parameters);
    Task SetReasonForNonCompetitiveTender(SetReasonForNonCompetitiveTenderParameters parameters);

    // Building Control

    Task<GetProgressReportBuildingControlKeyDatesResult> GetBuildingControlKeyDates(GetProgressReportBuildingControlKeyDatesParameters parameters);
    Task SetBuildingControlKeyDates(SetProgressReportBuildingControlKeyDatesParameters parameters);
    Task<GetBuildingControlDatesChangedResult> GetBuildingControlDatesChanged(GetBuildingControlDatesChangedParameters parameters);
    Task SetBuildingControlDatesChanged(SetBuildingControlDatesChangedParameters parameters);

    // Planning Permission

    Task<GetProgressReportWorksPlanningKeyDatesResult> GetProgressReportWorksPlanningKeyDates(GetProgressReportWorksPlanningKeyDatesParameters parameters);
    Task<GetProgressReportPlanningPermissionKeyDatesResult> GetProgressReportPlanningPermissionKeyDates(GetProgressReportPlanningPermissionKeyDatesParameters parameters);
    Task<GetProgressReportReasonNotAppliedPlanningPermissionResult> GetProgressReportReasonNotAppliedPlanningPermission(GetProgressReportReasonNotAppliedPlanningPermissionParameters parameters);
    Task<GetProgressReportTellUsAboutPlanningPermissionResult> GetProgressReportTellUsAboutPlanningPermission(GetProgressReportTellUsAboutPlanningPermissionParameters parameters);
    Task SetProgressReportReasonNotAppliedPlanningPermission(SetProgressReportReasonNotAppliedPlanningPermissionParameters parameters);
    Task SetProgressReportTellUsAboutPlanningPermission(SetProgressReportTellUsAboutPlanningPermissionParameters parameters);
    Task SetProgressReportPlanningPermissionKeyDates(SetProgressReportPlanningPermissionKeyDatesParameters parameters);
    Task<bool?> GetProgressReportHaveYouAppliedPlanningPermission(GetProgressReportHaveYouAppliedPlanningPermissionParameters parameters);
    Task SetProgressReportHaveYouAppliedPlanningPermission(SetProgressReportHaveYouAppliedPlanningPermissionParameters parameters);
    Task<GetPlanningPermissionDatesChangedResult> GetPlanningPermissionDatesChanged(GetPlanningPermissionDatesChangedParameters parameters);
    Task SetPlanningPermissionDatesChanged(SetPlanningPermissionDatesChangedParameters parameters);
    Task ClearPlanningPermissionSlippage(ClearPlanningPermissionSlippageParameters parameters);

    // Remediation

    Task<GetProgressReportRemediationKeyDatesResult> GetRemediationKeyDates(GetProgressReportRemediationKeyDatesParameters parameters);
    Task SetRemediationKeyDates(SetProgressReportRemediationKeyDatesParameters parameters);
    Task<GetRemediationDatesChangedResult> GetRemediationDatesChanged(GetRemediationDatesChangedParameters parameters);
    Task SetRemediationDatesChanged(SetRemediationDatesChangedParameters parameters);
}