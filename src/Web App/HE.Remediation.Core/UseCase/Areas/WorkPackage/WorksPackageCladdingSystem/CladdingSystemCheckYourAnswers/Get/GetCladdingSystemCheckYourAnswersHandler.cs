using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCladdingSystem.CladdingSystemCheckYourAnswers.Get;

public class GetCladdingSystemCheckYourAnswersHandler : IRequestHandler<GetCladdingSystemCheckYourAnswersRequest, GetCladdingSystemCheckYourAnswersResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IWorkPackageRepository _workPackageRepository;

    public GetCladdingSystemCheckYourAnswersHandler(IApplicationDataProvider applicationDataProvider,
                                                    IBuildingDetailsRepository buildingDetailsRepository,
                                                    IApplicationRepository applicationRepository,
                                                    IWorkPackageRepository workPackageRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _workPackageRepository = workPackageRepository;
    }

    public async Task<GetCladdingSystemCheckYourAnswersResponse> Handle(GetCladdingSystemCheckYourAnswersRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var checkMyAnswersResult = await _workPackageRepository.GetCostsScheduleCladdingSystemAnswers(request.FireRiskCladdingSystemsId);
        var isSubmitted = await _workPackageRepository.IsWorkPackageSubmitted();

        return new GetCladdingSystemCheckYourAnswersResponse
        {
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber,
            FireRiskCladdingSystemsId = request.FireRiskCladdingSystemsId,
            CladdingSystemIndex = request.CladdingSystemIndex,
            IsBeingRemoved = checkMyAnswersResult?.IsBeingRemoved,
            ReplacementCladdingSystemTypeName = checkMyAnswersResult?.ReplacementCladdingSystemTypeName,
            ReplacementOtherCladdingSystemType = checkMyAnswersResult?.ReplacementOtherCladdingSystemType,
            ReplacementCladdingManufacturerName = checkMyAnswersResult?.ReplacementCladdingManufacturerName,
            ReplacementOtherCladdingManufacturer = checkMyAnswersResult?.ReplacementOtherCladdingManufacturer,
            ReplacementInsulationTypeName = checkMyAnswersResult?.ReplacementInsulationTypeName,
            ReplacementOtherInsulationType = checkMyAnswersResult?.ReplacementOtherInsulationType,
            ReplacementInsulationManufacturerName = checkMyAnswersResult?.ReplacementInsulationManufacturerName,
            ReplacementOtherInsulationManufacturer = checkMyAnswersResult?.ReplacementOtherInsulationManufacturer,
            CladdingSystemArea= checkMyAnswersResult?.CladdingSystemArea,
            IsSubmitted = isSubmitted
        };
    }
}
