using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.Repositories.FireRiskAppraisal;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCladdingSystem.CladdingSystemDetails.Get;

public class GetCladdingSystemDetailsHandler : IRequestHandler<GetCladdingSystemDetailsRequest, GetCladdingSystemDetailsResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IFireRiskAppraisalRepository _fireRiskAppraisalRepository;
    private readonly IWorkPackageRepository _workPackageRepository;

    public GetCladdingSystemDetailsHandler(IApplicationDataProvider applicationDataProvider,
                                           IBuildingDetailsRepository buildingDetailsRepository,
                                           IApplicationRepository applicationRepository,
                                           IFireRiskAppraisalRepository fireRiskAppraisalRepository,
                                           IWorkPackageRepository workPackageRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _fireRiskAppraisalRepository = fireRiskAppraisalRepository;
        _workPackageRepository = workPackageRepository;
    }

    public async ValueTask<GetCladdingSystemDetailsResponse> Handle(GetCladdingSystemDetailsRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var claddingSystemResult = await _workPackageRepository.GetCostsScheduleCladdingSystemDetails(request.FireRiskCladdingSystemsId);
        var isSubmitted = await _workPackageRepository.IsWorkPackageSubmitted();

        var claddingTypes = await _fireRiskAppraisalRepository.GetCladdingSystemTypes();
        var insulationTypes = await _fireRiskAppraisalRepository.GetInsulationTypes();

        var manufacturers = await _fireRiskAppraisalRepository.GetCladdingManufacturers();
        var claddingManufacturers = manufacturers
            .OrderBy(x => x.IsEndOfList)
            .ThenBy(x => x.Name);

        var insulationManufacturers = manufacturers
            .OrderBy(x => x.IsEndOfList)
            .ThenBy(x => x.Name);

        return new GetCladdingSystemDetailsResponse
        {
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber,
            FireRiskCladdingSystemsId = request.FireRiskCladdingSystemsId,
            CladdingSystemIndex = request.CladdingSystemIndex,
            CladdingSystemTypeName = claddingSystemResult?.CladdingSystemTypeName,
            CladdingManufacturerName = claddingSystemResult?.CladdingManufacturerName,
            InsulationTypeName = claddingSystemResult?.InsulationTypeName,
            InsulationManufacturerName = claddingSystemResult?.InsulationManufacturerName,
            IsBeingRemoved = claddingSystemResult?.IsBeingRemoved,
            ReplacementCladdingSystemTypeId = claddingSystemResult?.ReplacementCladdingSystemTypeId,
            ReplacementInsulationTypeId = claddingSystemResult?.ReplacementInsulationTypeId,
            ReplacementCladdingManufacturerId = claddingSystemResult?.ReplacementCladdingManufacturerId,
            ReplacementInsulationManufacturerId = claddingSystemResult?.ReplacementInsulationManufacturerId,
            ReplacementOtherCladdingSystemType = claddingSystemResult?.ReplacementOtherCladdingSystemType,
            ReplacementOtherInsulationType = claddingSystemResult?.ReplacementOtherInsulationType,
            ReplacementOtherCladdingManufacturer = claddingSystemResult?.ReplacementOtherCladdingManufacturer,
            ReplacementOtherInsulationManufacturer = claddingSystemResult?.ReplacementOtherInsulationManufacturer,
            CladdingSystemArea = claddingSystemResult?.CladdingSystemArea,
            IsSubmitted = isSubmitted,
            CladdingTypes = claddingTypes,
            InsulationTypes = insulationTypes,
            CladdingManufacturers = claddingManufacturers,
            InsulationManufacturers = insulationManufacturers
        };
    }
}
