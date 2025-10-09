using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.ApprovalDateGateWayTwoApplication.Get;

using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.BuildingControlCheckYourAnswers.Get;

internal class GetCheckYourAnswersHandler : IRequestHandler<GetCheckYourAnswersRequest, GetCheckYourAnswersResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IScheduleOfWorksRepository _scheduleOfWorksRepository;

    public GetCheckYourAnswersHandler(
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

    public async Task<GetCheckYourAnswersResponse> Handle(GetCheckYourAnswersRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();
        var reference = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var isSubmitted = await _scheduleOfWorksRepository.IsScheduleOfWorksSubmitted();

        var approvalApplied = await _scheduleOfWorksRepository.GetScheduleOfWorksIsBuildingControlApprovalApplied();
        var resultApprovalDate = await _scheduleOfWorksRepository.GetScheduleOfWorksBuildingControlApprovalDate();
        var buildingControlFiles = await _scheduleOfWorksRepository.GetScheduleOfWorksBuildingControlFiles(applicationId);

        var taskStatusesResult = await _scheduleOfWorksRepository.GetScheduleOfWorksTaskStatuses();

        return new GetCheckYourAnswersResponse
        {
            ApplicationReferenceNumber = reference,
            BuildingName = buildingName,
            IsSubmitted = isSubmitted,
            IsBuildingControlApprovalApplied = approvalApplied.HasValue? (approvalApplied.Value ? ENoYes.Yes : ENoYes.No) : null,
            BuildingControlApprovalDate = resultApprovalDate,
            BuildingControlStatusId = taskStatusesResult?.BuildingControlStatusId ?? ETaskStatus.NotStarted,
            AddedFiles = buildingControlFiles.ToList()
        };
    }
}