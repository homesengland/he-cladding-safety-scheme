using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting.KeyDates;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.KeyDates;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.KeyDates
{
    public abstract class KeyDatesEntryPoint(
        IApplicationDataProvider dataProvider, 
        IProgressReportingKeyDatesRepository progressReportingKeyDatesRepository,
        IMonthlyProgressReportingRepository monthlyProgressReportingRepository)
    {
        private readonly IProgressReportingKeyDatesRepository _progressReportingKeyDatesRepository = progressReportingKeyDatesRepository;
        private readonly IApplicationDataProvider _dataProvider = dataProvider;
        private readonly IMonthlyProgressReportingRepository _monthlyProgressReportingRepository = monthlyProgressReportingRepository;

        protected async Task SetToInProgress()
        {
            var applicationId = _dataProvider.GetApplicationId();
            var progressReportId = _dataProvider.GetProgressReportId();

            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            await _progressReportingKeyDatesRepository.UpdateKeyDatesStatus(
                new SetKeyDatesStatusParameters() {
                    ApplicationId = applicationId,
                    ProgressReportId = progressReportId,
                    TaskStatusId = ETaskStatus.InProgress
                }
            );

            await _monthlyProgressReportingRepository.SetMonthlyReportStatus(
                new SetMonthlyReportStatusParameters
                {
                    ApplicationId = applicationId,
                    ProgressReportId = progressReportId,
                    TaskStatusId = (int)ETaskStatus.InProgress
                });

            scope.Complete();
        }
    }
}
