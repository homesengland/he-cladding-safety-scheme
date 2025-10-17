using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.BuildingSafetyRegulator.Get;

using MediatR;


namespace HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.ApprovalDateGateWayTwoApplication.Get;

public class GetApprovalDateHandler : IRequestHandler<GetApprovalDateRequest, GetApprovalDateResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IScheduleOfWorksRepository _scheduleOfWorksRepository;

    public GetApprovalDateHandler(
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

    public async Task<GetApprovalDateResponse> Handle(GetApprovalDateRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();
        var reference = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var isSubmitted = await _scheduleOfWorksRepository.IsScheduleOfWorksSubmitted();

        var result = await _scheduleOfWorksRepository.GetScheduleOfWorksBuildingControlApprovalDate();


        return new GetApprovalDateResponse
        {
            ApplicationReferenceNumber = reference,
            BuildingName = buildingName,
            IsSubmitted = isSubmitted,
            ApprovalDateDay = result?.Day,
            ApprovalDateMonth = result?.Month,
            ApprovalDateYear = result?.Year,
        };
    }
}