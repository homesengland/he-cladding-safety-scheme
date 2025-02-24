using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.ProjectDates.Get;

public class GetProjectDatesHandler : IRequestHandler<GetProjectDatesRequest, GetProjectDatesResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IScheduleOfWorksRepository _scheduleOfWorksRepository;

    public GetProjectDatesHandler(IApplicationDataProvider applicationDataProvider,
                                  IBuildingDetailsRepository buildingDetailsRepository,
                                  IApplicationRepository applicationRepository,
                                  IScheduleOfWorksRepository scheduleOfWorksRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _scheduleOfWorksRepository = scheduleOfWorksRepository;
    }

    public async Task<GetProjectDatesResponse> Handle(GetProjectDatesRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);
        var isSubmitted = await _scheduleOfWorksRepository.IsScheduleOfWorksSubmitted();

        var projectDates = await _scheduleOfWorksRepository.GetProjectDates();

        return new GetProjectDatesResponse
        {
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber,
            ExpectedProjectStartDate = projectDates?.ExpectedProjectStartDate,
            ExpectedProjectEndDate = projectDates?.ExpectedProjectEndDate,
            ProjectStartDateMonth = projectDates?.ProjectStartDate?.Month,
            ProjectStartDateYear = projectDates?.ProjectStartDate?.Year,
            ProjectEndDateMonth = projectDates?.ProjectEndDate?.Month,
            ProjectEndDateYear = projectDates?.ProjectEndDate?.Year,
            IsSubmitted = isSubmitted
        };
    }
}
