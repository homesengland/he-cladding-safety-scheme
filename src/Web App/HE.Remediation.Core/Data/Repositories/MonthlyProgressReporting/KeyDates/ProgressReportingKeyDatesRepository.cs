using Dapper;
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
using HE.Remediation.Core.Interface;

namespace HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting.KeyDates
{
    public class ProgressReportingKeyDatesRepository : IProgressReportingKeyDatesRepository
    {
        private readonly IDbConnectionWrapper _dbConnectionWrapper;

        public ProgressReportingKeyDatesRepository(IDbConnectionWrapper dbConnectionWrapper)
        {
            _dbConnectionWrapper = dbConnectionWrapper;
        }

        public async Task<ProgressReportKeyDatesResult> GetProgressReportKeyDates(Guid monthlyProgressReportId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@MonthlyProgressReportId", monthlyProgressReportId);

            ProgressReportKeyDatesResult result = null;

            await _dbConnectionWrapper.QueryAsync<ProgressReportKeyDatesResult, FileResult, ProgressReportKeyDatesResult>(
                nameof(GetProgressReportKeyDates),
                (keyDates, file) =>
                {
                    result ??= keyDates;

                    if (file is not null && result.BuildingControlDecisionFiles.All(x => x.Id != file.Id))
                    {
                        result.BuildingControlDecisionFiles.Add(file);
                    }

                    return result;
                },
                parameters);

            return result;
        }

        public async Task UpdateKeyDatesStatus(SetKeyDatesStatusParameters parameters)
        {
            await _dbConnectionWrapper.ExecuteAsync(nameof(UpdateKeyDatesStatus), parameters);
        }

        // Works Planning

        public async Task SetProgressReportWorksPlanningKeyDates(SetProgressReportWorksPlanningKeyDatesParameters parameters)
        {
            await _dbConnectionWrapper.ExecuteAsync(nameof(SetProgressReportWorksPlanningKeyDates), parameters);
        }

        public async Task<IReadOnlyCollection<GetProgressReportKeyDatesChangeTypesResult>> GetProgressReportKeyDatesChangeTypes()
        {
            var results = await _dbConnectionWrapper.QueryAsync<GetProgressReportKeyDatesChangeTypesResult>(nameof(GetProgressReportKeyDatesChangeTypes));
            return results;
        }

        public async Task<GetWorksPlanningDatesChangedResult> GetWorksPlanningDatesChanged(GetWorksPlanningDatesChangedParameters parameters)
        {
            var result = await _dbConnectionWrapper.QuerySingleOrDefaultAsync<GetWorksPlanningDatesChangedResult>(
                    nameof(GetWorksPlanningDatesChanged),
                    parameters);

            return result;
        }

        public async Task SetWorksPlanningDatesChanged(SetWorksPlanningDatesChangedParameters parameters)
        {
            await _dbConnectionWrapper.ExecuteAsync(nameof(SetWorksPlanningDatesChanged), parameters);
        }

        public async Task<GetContractorTenderDetailsResult> GetContractorTenderDetails(GetMonthlyProgressReportParameters parameters)
        {
            var result = await _dbConnectionWrapper.QuerySingleOrDefaultAsync<GetContractorTenderDetailsResult>(
                nameof(GetContractorTenderDetails),
                parameters);
            return result;
        }

        public async Task SetContractorTenderType(SetContractorTenderTypeParameters parameters)
        {
            await _dbConnectionWrapper.ExecuteAsync(nameof(SetContractorTenderType), parameters);
        }

        public async Task SetReasonForNonCompetitiveTender(SetReasonForNonCompetitiveTenderParameters parameters)
        {
            await _dbConnectionWrapper.ExecuteAsync(nameof(SetReasonForNonCompetitiveTender), parameters);
        }

        // Building Control

        public async Task<GetProgressReportBuildingControlKeyDatesResult> GetBuildingControlKeyDates(GetProgressReportBuildingControlKeyDatesParameters parameters)
        {
            var result = await _dbConnectionWrapper.QuerySingleOrDefaultAsync<GetProgressReportBuildingControlKeyDatesResult>("GetProgressReportBuildingControlKeyDates", parameters);
            return result;
        }

        public async Task<bool> SetBuildingControlKeyDates(SetProgressReportBuildingControlKeyDatesParameters parameters)
        {
            var isDateChange = await _dbConnectionWrapper.QuerySingleOrDefaultAsync<bool>("SetProgressReportBuildingControlKeyDates", parameters);
            return isDateChange;
        }

        public async Task<GetBuildingControlDatesChangedResult> GetBuildingControlDatesChanged(GetBuildingControlDatesChangedParameters parameters)
        {
            var result = await _dbConnectionWrapper.QuerySingleOrDefaultAsync<GetBuildingControlDatesChangedResult>(
                   nameof(GetBuildingControlDatesChanged),
                   parameters);

            return result;
        }

        public async Task SetBuildingControlDatesChanged(SetBuildingControlDatesChangedParameters parameters)
        {
            await _dbConnectionWrapper.ExecuteAsync(nameof(SetBuildingControlDatesChanged), parameters);
        }

        // Planning Permission

        public async Task<GetProgressReportWorksPlanningKeyDatesResult> GetProgressReportWorksPlanningKeyDates(GetProgressReportWorksPlanningKeyDatesParameters parameters)
        {
            var result = await _dbConnectionWrapper
                .QuerySingleOrDefaultAsync<GetProgressReportWorksPlanningKeyDatesResult>(
                    nameof(GetProgressReportWorksPlanningKeyDates),
                    parameters);

            return result;
        }

        public async Task<GetProgressReportPlanningPermissionKeyDatesResult> GetProgressReportPlanningPermissionKeyDates(GetProgressReportPlanningPermissionKeyDatesParameters parameters)
        {
            var result = await _dbConnectionWrapper
                .QuerySingleOrDefaultAsync<GetProgressReportPlanningPermissionKeyDatesResult>(
                    nameof(GetProgressReportPlanningPermissionKeyDates),
                    parameters);

            return result;
        }

        public async Task<GetProgressReportReasonNotAppliedPlanningPermissionResult> GetProgressReportReasonNotAppliedPlanningPermission(GetProgressReportReasonNotAppliedPlanningPermissionParameters parameters)
        {
            var result = await _dbConnectionWrapper
                .QuerySingleOrDefaultAsync<GetProgressReportReasonNotAppliedPlanningPermissionResult>(
                    nameof(GetProgressReportReasonNotAppliedPlanningPermission),
                    parameters);
            return result;
        }

        public async Task<GetProgressReportTellUsAboutPlanningPermissionResult> GetProgressReportTellUsAboutPlanningPermission(GetProgressReportTellUsAboutPlanningPermissionParameters parameters)
        {
            var result = await _dbConnectionWrapper
                .QuerySingleOrDefaultAsync<GetProgressReportTellUsAboutPlanningPermissionResult>(
                    nameof(GetProgressReportTellUsAboutPlanningPermission),
                    parameters);
            return result;
        }

        public async Task SetProgressReportReasonNotAppliedPlanningPermission(SetProgressReportReasonNotAppliedPlanningPermissionParameters parameters)
        {
            await _dbConnectionWrapper.ExecuteAsync(nameof(SetProgressReportReasonNotAppliedPlanningPermission), parameters);
        }

        public async Task<bool> SetProgressReportTellUsAboutPlanningPermission(SetProgressReportTellUsAboutPlanningPermissionParameters parameters)
        {
            var isDateChange = await _dbConnectionWrapper.QuerySingleOrDefaultAsync<bool>(nameof(SetProgressReportTellUsAboutPlanningPermission), parameters);
            return isDateChange;
        }

        public async Task SetProgressReportPlanningPermissionKeyDates(SetProgressReportPlanningPermissionKeyDatesParameters parameters)
        {
            await _dbConnectionWrapper.ExecuteAsync(nameof(SetProgressReportPlanningPermissionKeyDates), parameters);
        }

        public async Task<bool?> GetProgressReportHaveYouAppliedPlanningPermission(GetProgressReportHaveYouAppliedPlanningPermissionParameters parameters)
        {
            var result = await _dbConnectionWrapper
                .QuerySingleOrDefaultAsync<bool?>(
                    nameof(GetProgressReportHaveYouAppliedPlanningPermission),
                    parameters);

            return result;
        }

        public async Task SetProgressReportHaveYouAppliedPlanningPermission(SetProgressReportHaveYouAppliedPlanningPermissionParameters parameters)
        {
            await _dbConnectionWrapper.ExecuteAsync(nameof(SetProgressReportHaveYouAppliedPlanningPermission), parameters);
        }

        public async Task<GetPlanningPermissionDatesChangedResult> GetPlanningPermissionDatesChanged(GetPlanningPermissionDatesChangedParameters parameters)
        {
            var result = await _dbConnectionWrapper.QuerySingleOrDefaultAsync<GetPlanningPermissionDatesChangedResult>(
                           nameof(GetPlanningPermissionDatesChanged),
                           parameters);

            return result;
        }

        public async Task SetPlanningPermissionDatesChanged(SetPlanningPermissionDatesChangedParameters parameters)
        {
            await _dbConnectionWrapper.ExecuteAsync(nameof(SetPlanningPermissionDatesChanged), parameters);
        }

        public async Task ClearPlanningPermissionSlippage(ClearPlanningPermissionSlippageParameters parameters)
        {
            await _dbConnectionWrapper.ExecuteAsync(nameof(ClearPlanningPermissionSlippage), parameters);
        }

        // Remediation

        public async Task<GetProgressReportRemediationKeyDatesResult> GetRemediationKeyDates(GetProgressReportRemediationKeyDatesParameters parameters)
        {
            var result = await _dbConnectionWrapper.QuerySingleOrDefaultAsync<GetProgressReportRemediationKeyDatesResult>("GetProgressReportRemediationKeyDates", parameters);
            return result;
        }

        public async Task<bool> SetRemediationKeyDates(SetProgressReportRemediationKeyDatesParameters parameters)
        {
            var isDateChange = await _dbConnectionWrapper.QuerySingleOrDefaultAsync<bool>("SetProgressReportRemediationKeyDates", parameters);
            return isDateChange;
        }

        public async Task<GetRemediationDatesChangedResult> GetRemediationDatesChanged(GetRemediationDatesChangedParameters parameters)
        {
            var result = await _dbConnectionWrapper.QuerySingleOrDefaultAsync<GetRemediationDatesChangedResult>(
                   nameof(GetRemediationDatesChanged),
                   parameters);

            return result;
        }

        public async Task SetRemediationDatesChanged(SetRemediationDatesChangedParameters parameters)
        {
            await _dbConnectionWrapper.ExecuteAsync(nameof(SetRemediationDatesChanged), parameters);
        }

        public async Task<GetProgressReportKeyDatesChangeFlagsResult> GetProgressReportKeyDatesChangeFlags(GetMonthlyProgressReportParameters parameters)
        {
            var result = await _dbConnectionWrapper.QuerySingleOrDefaultAsync<GetProgressReportKeyDatesChangeFlagsResult>(
                nameof(GetProgressReportKeyDatesChangeFlags),
                parameters);

            if (result == null)
            {
                result = new GetProgressReportKeyDatesChangeFlagsResult
                {
                    WorksPlanningDatesChanged = false,
                    BuildingControlDatesChanged = false,
                    PlanningPermissionDatesChanged = false,
                    RemediationDatesChanged = false
                };
            }

            return result;
        }
    }
}
