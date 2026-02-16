using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.Communication;
using HE.Remediation.Core.Services.StatusTransition;
using Mediator;
using System.Transactions;

namespace HE.Remediation.Core.UseCase.Areas.Application.Submit.SetSubmit
{
    public class SetSubmitHandler : IRequestHandler<SetSubmitRequest>
    {
        private readonly IDbConnectionWrapper _dbConnectionWrapper;
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IApplicationRepository _applicationRepository;
        private readonly ITaskRepository _taskRepository;
        private readonly IWorkPackageRepository _workPackageRepository;
        private readonly ICommunicationService _communicationService;
        private readonly IStatusTransitionService _statusTransitionService;

        public SetSubmitHandler(IDbConnectionWrapper dbConnectionWrapper,
            IApplicationDataProvider applicationDataProvider,
            IApplicationRepository applicationRepository,
            ITaskRepository taskRepository,
            IWorkPackageRepository workPackageRepository,
            ICommunicationService communicationService,
            IStatusTransitionService statusTransitionService)
        {
            _dbConnectionWrapper = dbConnectionWrapper;
            _applicationDataProvider = applicationDataProvider;
            _applicationRepository = applicationRepository;
            _taskRepository = taskRepository;
            _workPackageRepository = workPackageRepository;
            _communicationService = communicationService;
            _statusTransitionService = statusTransitionService;
            ;
        }

        public async ValueTask<Unit> Handle(SetSubmitRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();
            await UpdateSubmitRequest(applicationId);

            return Unit.Value;
        }


        private async Task UpdateSubmitRequest(Guid applicationId)
        {
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            await _dbConnectionWrapper.ExecuteAsync("UpdateApplicationSubmit", new
            {
                ApplicationId = applicationId
            });

            await _statusTransitionService.TransitionToInternalStatus(EApplicationInternalStatus.FinalApplicationSubmitted, applicationIds: applicationId);

            if (_applicationDataProvider.GetApplicationScheme() == EApplicationScheme.CladdingSafetyScheme)
            {
                var taskType = await _taskRepository.GetTaskType(new GetTaskTypeParameters("Eligibility", "Start eligibility checks"));

                var referenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);

                await _taskRepository.InsertTask(new InsertTaskParameters
                {
                    ReferenceId = applicationId,
                    AssignedToTeamId = (int)ETeam.DaviesOps,
                    AssignedToUserId = null,
                    CreatedByUserId = null,
                    Description = $"Application {referenceNumber} is ready for eligibility checks",
                    RequiredByDate = DateOnly.FromDateTime(DateTime.UtcNow),
                    TaskStatus = ETaskStatus.NotStarted.ToString(),
                    TaskTypeId = taskType.Id
                });

                await _communicationService.QueueEmailCommunication(new EmailCommunicationRequest
                (
                    applicationId,
                    EEmailType.ApplicationSubmitted
                ));
            }
            else if (_applicationDataProvider.GetApplicationScheme() == EApplicationScheme.SocialSector)
            {
                /* 
                 * Scheme = SSSF / Stage = 'Apply For Grant' / Status = 'Application Submitted'
                 * 
                 * Note: 'GetApplicationsForMonthlyReporting' sproc will generate Progress Report when WorksAlreadyCompleted = false
                 * 
                 * Note: Application Stage Diagram / Index will display 'Works Package' instantly
                 *       GenerateProgressReportJob will only generate Progress Report if WorksAlreadyCompleted = false
                 * So:
                 *       WorksAlreadyCompleted  = WP
                 *       !WorksAlreadyCompleted = WP + PR
                 */

                await _workPackageRepository.InsertWorkPackage(applicationId);

                // Queue email based on building remediation status
                var worksAlreadyCompleted = await _dbConnectionWrapper.QuerySingleOrDefaultAsync<bool?>("GetBuildingWorksAlreadyCompleted", new
                {
                    ApplicationId = applicationId
                });

                var emailType = worksAlreadyCompleted == true
                    ? EEmailType.SssfBuildingCompleteApplicationSubmitted
                    : EEmailType.SssfBuildingNonCompleteApplicationSubmitted;

                await _communicationService.QueueEmailCommunication(new EmailCommunicationRequest
                (
                    applicationId,
                    emailType
                ));
            }

            scope.Complete();
        }
    }
}
