using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

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

    public async Task<GetCheckYourAnswersResponse> Handle(GetCheckYourAnswersRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);
        var isSubmitted = await _scheduleOfWorksRepository.IsScheduleOfWorksSubmitted();

        var answers = await _scheduleOfWorksRepository.GetCheckYourAnswers();
        var contractFileNames = await _scheduleOfWorksRepository.GetContractFileNames();
        var buildingControlFileNames = (await _scheduleOfWorksRepository.GetScheduleOfWorksBuildingControlFiles(applicationId)).Select(x => x.Name).ToArray();
        var leaseholderEngagementFileNames = (await _scheduleOfWorksRepository.GetScheduleOfWorksLeaseholderEngagementFiles(applicationId)).Select(x => x.Name).ToArray();

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
            ContractFileNames = contractFileNames,
            BuildingControlFileNames = buildingControlFileNames,
            LeaseholderEngagementFileNames = leaseholderEngagementFileNames,
            IsSubmitted = isSubmitted
        };
    }
}
