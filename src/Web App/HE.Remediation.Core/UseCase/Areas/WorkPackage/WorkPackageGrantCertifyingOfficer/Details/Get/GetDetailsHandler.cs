using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageGrantCertifyingOfficer.Details.Get;

public class GetDetailsHandler : IRequestHandler<GetDetailsRequest, GetDetailsResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IWorkPackageRepository _workPackageRepository;

    public GetDetailsHandler(IApplicationDataProvider applicationDataProvider,
                            IBuildingDetailsRepository buildingDetailsRepository,
                            IApplicationRepository applicationRepository,
                            IWorkPackageRepository workPackageRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _workPackageRepository = workPackageRepository;
    }

    public async ValueTask<GetDetailsResponse> Handle(GetDetailsRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var grantCertifyingOfficer = await _workPackageRepository.GetGrantCertifyingOfficerDetails();

        var isSubmitted = await _workPackageRepository.IsWorkPackageSubmitted();

        return (ECertifyingOfficerResponse)grantCertifyingOfficer.CertifyingOfficerResponseId == ECertifyingOfficerResponse.No
            ? new GetDetailsResponse
            {
                BuildingName = buildingName,
                ApplicationReferenceNumber = applicationReferenceNumber,
                RoleName = grantCertifyingOfficer?.RoleName,
                IsSubmitted = isSubmitted
            }
            : new GetDetailsResponse
            {
                BuildingName = buildingName,
                ApplicationReferenceNumber = applicationReferenceNumber,
                RoleName = grantCertifyingOfficer?.RoleName,
                Name = grantCertifyingOfficer?.Name,
                CompanyName = grantCertifyingOfficer?.CompanyName,
                CompanyRegistrationNumber = grantCertifyingOfficer?.CompanyRegistrationNumber,
                EmailAddress = grantCertifyingOfficer?.EmailAddress,
                PrimaryContactNumber = grantCertifyingOfficer?.PrimaryContactNumber,
                ContractSigned = grantCertifyingOfficer?.ContractSigned,
                IndemnityInsurance = grantCertifyingOfficer?.IndemnityInsurance,
                IndemnityInsuranceReason = grantCertifyingOfficer.IndemnityInsuranceReason,
                InvolvedInOriginalInstallation = grantCertifyingOfficer?.InvolvedInOriginalInstallation,
                InvolvedRoleReason = grantCertifyingOfficer.InvolvedRoleReason,
                IsSubmitted = isSubmitted
            };
    }
}
