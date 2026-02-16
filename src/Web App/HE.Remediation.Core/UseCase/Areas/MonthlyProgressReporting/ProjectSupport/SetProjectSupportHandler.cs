using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.ProjectSupport;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.MonthlyProgressReportingProjectPlan;
using Mediator;
using Microsoft.Extensions.Logging;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectSupport
{
    public class SetProjectSupportHandler : IRequestHandler<SetProjectSupportRequest>
    {
        private readonly IProgressReportingProjectSupportRepository _progressReportingProjectSupportRepository;
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IMonthlyProgressReportingRepository _monthlyProgressReportingRepository;

        public SetProjectSupportHandler(
            IProgressReportingProjectSupportRepository progressReportingProjectSupportRepository,
            IApplicationDataProvider applicationDataProvider, 
            IMonthlyProgressReportingRepository monthlyProgressReportingRepository)
        {
            _progressReportingProjectSupportRepository = progressReportingProjectSupportRepository;
            _applicationDataProvider = applicationDataProvider;
            _monthlyProgressReportingRepository = monthlyProgressReportingRepository;
        }

        public async ValueTask<Unit> Handle(SetProjectSupportRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();
            var progressReportId = _applicationDataProvider.GetProgressReportId();

            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            var parameters = new SetProjectSupportParameters
            {
                ApplicationId = applicationId,
                ProgressReportId = progressReportId,
                RequiresSupport = request.RequiresSupport == ENoYes.Yes,
                TaskStatusId = (int)ETaskStatus.InProgress
            };

            await _progressReportingProjectSupportRepository.SetProjectSupportDetails(parameters);

            await _monthlyProgressReportingRepository.SetMonthlyReportStatus(new SetMonthlyReportStatusParameters
            {
                ApplicationId = applicationId,
                ProgressReportId = progressReportId,
                TaskStatusId = (int)ETaskStatus.InProgress
            });

            scope.Complete();
            return Unit.Value;
        }
    }
}