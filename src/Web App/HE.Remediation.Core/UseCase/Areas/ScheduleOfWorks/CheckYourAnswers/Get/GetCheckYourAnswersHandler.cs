using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.CheckYourAnswers.Get;

public class GetCheckYourAnswersHandler : IRequestHandler<GetCheckYourAnswersRequest, GetCheckYourAnswersResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IScheduleOfWorksRepository _scheduleOfWorksRepository;

    public GetCheckYourAnswersHandler(IApplicationDataProvider applicationDataProvider,
                                      IBuildingDetailsRepository buildingDetailsRepository,
                                      IApplicationRepository applicationRepository,
                                      IScheduleOfWorksRepository scheduleOfWorksRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _scheduleOfWorksRepository = scheduleOfWorksRepository;
    }

    public async ValueTask<GetCheckYourAnswersResponse> Handle(GetCheckYourAnswersRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);
        var isSubmitted = await _scheduleOfWorksRepository.IsScheduleOfWorksSubmitted();

        var answers = await _scheduleOfWorksRepository.GetCheckYourAnswers();

        return new GetCheckYourAnswersResponse
        {
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber,
            ProjectStartDate = answers?.ProjectStartDate,
            ProjectEndDate = answers?.ProjectEndDate,
            Duration = answers?.Duration,
            ApprovedGrantFunding = answers?.ApprovedGrantFunding,
            GrantPaidToDate = answers?.GrantPaidToDate,
            ProfiledPayments = answers?.ProfiledPayments,
            IsSubmitted = isSubmitted
        };
    }
}
