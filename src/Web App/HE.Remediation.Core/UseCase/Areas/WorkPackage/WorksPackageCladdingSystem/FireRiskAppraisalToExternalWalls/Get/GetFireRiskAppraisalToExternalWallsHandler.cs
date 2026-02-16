using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCladdingSystem.FireRiskAppraisalToExternalWalls.Get;

public class GetFireRiskAppraisalToExternalWallsHandler : IRequestHandler<GetFireRiskAppraisalToExternalWallsRequest, GetFireRiskAppraisalToExternalWallsResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IWorkPackageRepository _workPackageRepository;

    public GetFireRiskAppraisalToExternalWallsHandler(IApplicationDataProvider applicationDataProvider,
                                                      IBuildingDetailsRepository buildingDetailsRepository,
                                                      IApplicationRepository applicationRepository,
                                                      IWorkPackageRepository workPackageRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _workPackageRepository = workPackageRepository;
    }

    public async ValueTask<GetFireRiskAppraisalToExternalWallsResponse> Handle(GetFireRiskAppraisalToExternalWallsRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var costScheduleStatus = await _workPackageRepository.GetCostsScheduleStatus();
        if (costScheduleStatus is null)
        {
            await _workPackageRepository.InsertCostsSchedule();
        }

        var fraewCladdingDetails = await _workPackageRepository.GetCostsScheduleFireRiskCladdingWorks();
        var requiresSubcontractors = await _workPackageRepository.GetCostsScheduleRequiresSubcontractors();
        var isSubmitted = await _workPackageRepository.IsWorkPackageSubmitted();

        return new GetFireRiskAppraisalToExternalWallsResponse
        {
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber,
            FraewSurveyDate = fraewCladdingDetails?.FraewSurveyDate,
            FraewSurveyCost = fraewCladdingDetails?.FraewSurveyCost,
            FraewRemediationType = fraewCladdingDetails?.FraewRemediationType,
            CladdingSystems = fraewCladdingDetails?.CladdingWorks.ToList(),
            RequiresSubcontractors = requiresSubcontractors,
            IsSubmitted = isSubmitted
        };
    }
}
