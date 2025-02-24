using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageDeclaration.Declaration.Get;

public class GetDeclarationHandler : IRequestHandler<GetDeclarationRequest, GetDeclarationResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IWorkPackageRepository _workPackageRepository;

    public GetDeclarationHandler(IApplicationDataProvider applicationDataProvider,
                                   IBuildingDetailsRepository buildingDetailsRepository,
                                   IApplicationRepository applicationRepository,
                                   IWorkPackageRepository workPackageRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _workPackageRepository = workPackageRepository;
    }

    public async Task<GetDeclarationResponse> Handle(GetDeclarationRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var declarationResult = await _workPackageRepository.GetDeclaration();
        var isSubmitted = await _workPackageRepository.IsWorkPackageSubmitted();

        return new GetDeclarationResponse
        {
            AllCostsReasonable = declarationResult?.AllCostsReasonable,
            AllContractualRequirementsMet = declarationResult?.AllContractualRequirementsMet,
            AllCostsSetOutInFull = declarationResult?.AllCostsSetOutInFull,
            AcceptGrantAwardBasedOnCosts = declarationResult?.AcceptGrantAwardBasedOnCosts,
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber,
            IsSubmitted = isSubmitted
        };
    }
}
