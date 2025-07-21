using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WithdrawalRequest.SetFinalCheckYourAnswers
{
    public class SetWithdrawalRequestFinalCheckYourAnswersHandler : IRequestHandler<SetWithdrawalRequestFinalCheckYourAnswersRequest, Unit>
    {
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IApplicationRepository _applicationRepository;
        private readonly ITaskRepository _taskRepository;
        private readonly IDateRepository _dateRepository;

        public SetWithdrawalRequestFinalCheckYourAnswersHandler(
            IApplicationDataProvider applicationDataProvider,
            IApplicationRepository applicationRepository,
            ITaskRepository taskRepository,
            IDateRepository dateRepository)
        {
            _applicationDataProvider = applicationDataProvider;
            _applicationRepository = applicationRepository;
            _taskRepository = taskRepository;
            _dateRepository = dateRepository;
        }

        public async Task<Unit> Handle(SetWithdrawalRequestFinalCheckYourAnswersRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var reasonForClosing = await _applicationRepository.GetApplicationReasonForWithdrawalRequest(applicationId);

            var taskType = await _taskRepository.GetTaskType(new GetTaskTypeParameters("Application", "Review request to withdraw application"));
            var dueDate = await _dateRepository.AddWorkingDays(new AddWorkingDaysParameters
            {
                Date = DateTime.UtcNow.Date,
                WorkingDays = 1
            });

            await _taskRepository.InsertTask(new InsertTaskParameters
            {
                ReferenceId = applicationId,
                AssignedToTeamId = (int)ETeam.DaviesOps,
                AssignedToUserId = null,
                CreatedByUserId = null,
                Description = $"Review request to withdraw application:{Environment.NewLine}•Explain the reason for requesting to close this application{Environment.NewLine}•{reasonForClosing}",
                RequiredByDate = DateOnly.FromDateTime(dueDate),
                TaskStatus = ETaskStatus.NotStarted.ToString(),
                TopicId = taskType.TopicId,
                TaskTypeId = taskType.Id
            });

            return Unit.Value;

        }
    }
}

