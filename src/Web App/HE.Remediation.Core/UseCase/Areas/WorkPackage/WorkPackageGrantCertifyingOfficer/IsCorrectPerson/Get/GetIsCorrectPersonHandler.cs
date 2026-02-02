using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageGrantCertifyingOfficer.IsCorrectPerson.Get;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageGrantCertifyingOfficer.CorrectPerson.Get;

public class GetIsCorrectPersonHandler : IRequestHandler<GetIsCorrectPersonRequest, GetIsCorrectPersonResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IWorkPackageRepository _workPackageRepository;

    public GetIsCorrectPersonHandler(IApplicationDataProvider applicationDataProvider,
                                                       IBuildingDetailsRepository buildingDetailsRepository,
                                                       IApplicationRepository applicationRepository,
                                                       IWorkPackageRepository workPackageRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _workPackageRepository = workPackageRepository;
    }

    public async ValueTask<GetIsCorrectPersonResponse> Handle(GetIsCorrectPersonRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var isCorrectPersonResult = await _workPackageRepository.GetGrantCertifyingOfficerIsCorrectPerson();
        
        var isSubmitted = await _workPackageRepository.IsWorkPackageSubmitted();

        return new GetIsCorrectPersonResponse
        {
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber,
            CertifyingOfficerResponse = isCorrectPersonResult.CertifyingOfficerResponseId,
            IsSubmitted = isSubmitted
        };
    }
}