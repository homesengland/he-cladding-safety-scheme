using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageGrantCertifyingOfficer.AddressDetails.Get;

public class GetAddressDetailsHandler : IRequestHandler<GetAddressDetailsRequest, GetAddressDetailsResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IWorkPackageRepository _workPackageRepository;

    public GetAddressDetailsHandler(IApplicationDataProvider applicationDataProvider,
                                                       IBuildingDetailsRepository buildingDetailsRepository,
                                                       IApplicationRepository applicationRepository,
                                                       IWorkPackageRepository workPackageRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _workPackageRepository = workPackageRepository;
    }

    public async ValueTask<GetAddressDetailsResponse> Handle(GetAddressDetailsRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var address = await _workPackageRepository.GetGrantCertifyingOfficerAddress();
        var isCorrectPersonResult = await _workPackageRepository.GetGrantCertifyingOfficerIsCorrectPerson();

        return new GetAddressDetailsResponse
        {
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber,
            NameNumber = address?.NameNumber,
            AddressLine1 = address?.AddressLine1,
            AddressLine2 = address?.AddressLine2,
            City = address?.City,
            County = address?.County,
            Postcode = address?.Postcode,
            CertifyingOfficerResponse = isCorrectPersonResult.CertifyingOfficerResponseId
        };
    }
}