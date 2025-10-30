using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.Interface;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Transactions;
using Microsoft.Extensions.Options;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks;

public class SetLeaseholderEngagementHandler : IRequestHandler<SetLeaseholderEngagementRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IScheduleOfWorksRepository _scheduleOfWorksRepository;

    public SetLeaseholderEngagementHandler(
        IApplicationDataProvider applicationDataProvider,
        IScheduleOfWorksRepository scheduleOfWorksRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _scheduleOfWorksRepository = scheduleOfWorksRepository;
    }

    public async Task<Unit> Handle(SetLeaseholderEngagementRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();

        var taskStatusesResult = await _scheduleOfWorksRepository.GetScheduleOfWorksTaskStatuses();

        if (taskStatusesResult?.LeaseholderEngagementStatusId != ETaskStatus.Completed)
        {
            await _scheduleOfWorksRepository.UpdateScheduleOfWorksLeaseholderEngagementStatus(ETaskStatus.Completed);
        }

        return Unit.Value;
    }
}

public class SetLeaseholderEngagementRequest : IRequest
{
    private SetLeaseholderEngagementRequest()
    {
    }

    public static readonly SetLeaseholderEngagementRequest Request = new();
}