using Azure.Core;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.Communication;
using MediatR;
using System.Transactions;

namespace HE.Remediation.Core.UseCase.Areas.Application.Submit.SetSubmit
{
    public class SetSubmitHandler : IRequestHandler<SetSubmitRequest>
    {
        private readonly IDbConnectionWrapper _dbConnectionWrapper;
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IApplicationRepository _applicationRepository;
        private readonly ITaskRepository _taskRepository;
        private readonly ICommunicationService _communicationService;


        public SetSubmitHandler(IDbConnectionWrapper dbConnectionWrapper,
            IApplicationDataProvider applicationDataProvider,
            IApplicationRepository applicationRepository,
            ITaskRepository taskRepository,
            ICommunicationService communicationService)
        {
            _dbConnectionWrapper = dbConnectionWrapper;
            _applicationDataProvider = applicationDataProvider;
            _applicationRepository = applicationRepository;
            _taskRepository = taskRepository;
            _communicationService = communicationService; ;
        }

        public async Task<Unit> Handle(SetSubmitRequest request, CancellationToken cancellationToken)
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

            await _applicationRepository.UpdateInternalStatus(applicationId, EApplicationInternalStatus.FinalApplicationSubmitted);

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

            scope.Complete();
        }
    }
}
