using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.BuildingSafetyRegulator.Get;

public class GetBuildingControlApprovalHandler : IRequestHandler<GetBuildingControlApprovalRequest, GetBuildingControlApprovalResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IScheduleOfWorksRepository _scheduleOfWorksRepository;

    public GetBuildingControlApprovalHandler(
        IApplicationDataProvider applicationDataProvider,
        IApplicationRepository applicationRepository,
        IBuildingDetailsRepository buildingDetailsRepository,
        IScheduleOfWorksRepository scheduleOfWorksRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _applicationRepository = applicationRepository;
        _buildingDetailsRepository = buildingDetailsRepository;
        _scheduleOfWorksRepository = scheduleOfWorksRepository;
    }

    public async Task<GetBuildingControlApprovalResponse> Handle(GetBuildingControlApprovalRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();
        var reference = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var isSubmitted = await _scheduleOfWorksRepository.IsScheduleOfWorksSubmitted();

        var result = await _scheduleOfWorksRepository.GetScheduleOfWorksIsBuildingControlApprovalApplied();

        if (!isSubmitted)
        {
            var taskStatusesResult = await _scheduleOfWorksRepository.GetScheduleOfWorksTaskStatuses();

            if (taskStatusesResult?.BuildingControlStatusId == null)
            {
                await _scheduleOfWorksRepository.UpdateScheduleOfWorksBuildingControlStatus(ETaskStatus.InProgress);
            }
        }


        return new GetBuildingControlApprovalResponse
        {
            ApplicationReferenceNumber = reference,
            BuildingName = buildingName,
            IsSubmitted = isSubmitted,
            IsBuildingControlApprovalApplied = result.HasValue ? (result.Value ? ENoYes.Yes : ENoYes.No) : (ENoYes?)null
        };
    }
}