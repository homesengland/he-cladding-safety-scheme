using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageGrantCertifyingOfficer.Confirm.Get;

public class GetConfirmHandler : IRequestHandler<GetConfirmRequest, GetConfirmResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IWorkPackageRepository _workPackageRepository;

    public GetConfirmHandler(IApplicationDataProvider applicationDataProvider,
                            IBuildingDetailsRepository buildingDetailsRepository,
                            IApplicationRepository applicationRepository,
                            IWorkPackageRepository workPackageRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _workPackageRepository = workPackageRepository;
    }

    public async ValueTask<GetConfirmResponse> Handle(GetConfirmRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var grantCertifyingOfficer = await _workPackageRepository.GetGrantCertifyingOfficerDetails();

        var isSubmitted = await _workPackageRepository.IsWorkPackageSubmitted();

        return new GetConfirmResponse
        {
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber,
            RoleName = grantCertifyingOfficer.RoleName,
            Name = grantCertifyingOfficer.Name,
            CompanyName = grantCertifyingOfficer.CompanyName,
            CompanyRegistrationNumber = grantCertifyingOfficer.CompanyRegistrationNumber,
            EmailAddress = grantCertifyingOfficer.EmailAddress,
            PrimaryContactNumber = grantCertifyingOfficer.PrimaryContactNumber,
            ContractSigned = grantCertifyingOfficer.ContractSigned,
            IndemnityInsurance = grantCertifyingOfficer.IndemnityInsurance,
            IndemnityInsuranceReason = grantCertifyingOfficer.IndemnityInsuranceReason,
            InvolvedInOriginalInstallation = grantCertifyingOfficer.InvolvedInOriginalInstallation,
            InvolvedRoleReason = grantCertifyingOfficer.InvolvedRoleReason,
            CertifyingOfficerResponse = (ECertifyingOfficerResponse)grantCertifyingOfficer.CertifyingOfficerResponseId,
            IsSubmitted = isSubmitted
        };
    }
}
