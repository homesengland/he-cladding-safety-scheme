using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetTeamMember;

public class GetTeamMemberHandler : IRequestHandler<GetTeamMemberRequest, GetTeamMemberResponse>
{
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IPaymentRequestRepository _paymentRequestRepository;
    private readonly IWorkPackageRepository _workPackageRepository;

    public GetTeamMemberHandler(
        IBuildingDetailsRepository buildingDetailsRepository, 
        IApplicationRepository applicationRepository, 
        IApplicationDataProvider applicationDataProvider, 
        IPaymentRequestRepository paymentRequestRepository,
        IWorkPackageRepository workPackageRepository)
    {
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _applicationDataProvider = applicationDataProvider;        
        _paymentRequestRepository = paymentRequestRepository;
        _workPackageRepository = workPackageRepository;
    }

    public async Task<GetTeamMemberResponse> Handle(GetTeamMemberRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        var paymentRequestId = _applicationDataProvider.GetPaymentRequestId();

        var referenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var teamMember = await _workPackageRepository.GetTeamMember(request.TeamMemberId);

        if (teamMember is null && request.TeamMemberId.HasValue)
        {
            throw new EntityNotFoundException("Team Member not found");
        }

        var isSubmitted = await _paymentRequestRepository.IsPaymentRequestSubmitted(paymentRequestId);
        var isExpired = await _paymentRequestRepository.IsPaymentRequestExpired(paymentRequestId);

        return new GetTeamMemberResponse
        {
            IsSubmitted = isSubmitted,
            IsExpired = isExpired,
            Role = teamMember?.Role ?? request.TeamRole,
            OtherRole = teamMember?.OtherRole,
            ApplicationReferenceNumber = referenceNumber,
            BuildingName = buildingName,
            CompanyName = teamMember?.CompanyName,
            CompanyRegistration = teamMember?.CompanyRegistration,
            ConsiderateConstructorSchemeType = teamMember?.ConsiderateConstructorSchemeType,
            ContractSigned = teamMember?.ContractSigned,
            EmailAddress = teamMember?.EmailAddress,
            IndemnityInsurance = teamMember?.IndemnityInsurance,
            IndemnityInsuranceReason = teamMember?.IndemnityInsuranceReason,
            TeamMemberId = teamMember?.TeamMemberId,
            InvolvedInOriginalInstallation = teamMember?.InvolvedInOriginalInstallation,
            PrimaryContactNumber = teamMember?.PrimaryContactNumber,
            InvolvedRoleReason = teamMember?.InvolvedRoleReason,
            Name = teamMember?.Name,
            HasChasCertification = teamMember?.HasChasCertification
        };
    }
}
