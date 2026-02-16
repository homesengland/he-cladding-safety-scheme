using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting;

public class SetGrantCertifyingOfficerSignatoryHandler : IRequestHandler<SetGrantCertifyingOfficerSignatoryRequest>
{
    private readonly IProgressReportingRepository _progressReportingRepository;
    private readonly ITaskRepository _taskRepository;
    private readonly IApplicationDataProvider _applicationDataProvider;

    public SetGrantCertifyingOfficerSignatoryHandler(
        IProgressReportingRepository progressReportingRepository, 
        ITaskRepository taskRepository, 
        IApplicationDataProvider applicationDataProvider)
    {
        _progressReportingRepository = progressReportingRepository;
        _taskRepository = taskRepository;
        _applicationDataProvider = applicationDataProvider;
    }

    public async ValueTask<Unit> Handle(SetGrantCertifyingOfficerSignatoryRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _progressReportingRepository.UpdateGrantCertifyingOfficerSignatory(
            new UpdateGrantCertifyingOfficerSignatoryParameters
            {
                Signatory = request.Signatory,
                EmailAddress = request.EmailAddress,
                DateAppointed = new DateTime(request.DateAppointedYear!.Value, request.DateAppointedMonth!.Value, request.DateAppointedDay!.Value)
            });

        var taskRaised = await _progressReportingRepository.GetDutyOfCareDeedTaskRaised();

        if (!taskRaised)
        {
            var taskType = await _taskRepository.GetTaskType(new GetTaskTypeParameters(
                "Progress Report",
                "Progress Report GCO Duty of care deed"));

            await _taskRepository.InsertTask(new InsertTaskParameters
            {
                ReferenceId = _applicationDataProvider.GetApplicationId(),
                AssignedToTeamId = (int)ETeam.DaviesOps,
                Description = "Send Duty of care deed to Grant Certifying Officer",
                RequiredByDate = DateOnly.FromDateTime(DateTime.Today),
                TaskStatus = ETaskStatus.NotStarted.ToString(),
                TaskTypeId = taskType.Id,
                Notes = null
            });

            await _progressReportingRepository.SetDutyOfCareDeedTaskRaised(true);
        }

        scope.Complete();

        return Unit.Value;
    }
}

public class SetGrantCertifyingOfficerSignatoryRequest : IRequest
{
    public string Signatory { get; set; }
    public string EmailAddress { get; set; }
    public int? DateAppointedDay { get; set; }
    public int? DateAppointedMonth { get; set; }
    public int? DateAppointedYear { get; set; }
}