using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.LeadDesignerCompanyDetails.GetLeadDesignerCompanyDetails;

public class GetLeadDesignerCompanyDetailsHandler : IRequestHandler<GetLeadDesignerCompanyDetailsRequest, GetLeadDesignerCompanyDetailsResponse>
{
    private readonly IDbConnectionWrapper _connection;
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IProgressReportingRepository _progressReportingRepository;

    public GetLeadDesignerCompanyDetailsHandler(IDbConnectionWrapper connection,
                                         IApplicationDataProvider applicationDataProvider,
                                         IBuildingDetailsRepository buildingDetailsRepository,
                                         IApplicationRepository applicationRepository,
                                         IProgressReportingRepository progressReportingRepository)
    {
        _connection = connection;
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _progressReportingRepository = progressReportingRepository;
    }

    public async Task<GetLeadDesignerCompanyDetailsResponse> Handle(GetLeadDesignerCompanyDetailsRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        TeamMemberDetails teamMemberDetails = await _progressReportingRepository.GetLeadDesignerCompanyDetails();

        return new GetLeadDesignerCompanyDetailsResponse
        {
            Id = teamMemberDetails?.Id,
            CompanyName = teamMemberDetails?.CompanyName,
            CompanyRegistrationNumber = teamMemberDetails?.CompanyRegistrationNumber,
            Name = teamMemberDetails?.Name,
            EmailAddress = teamMemberDetails?.EmailAddress,
            PrimaryContactNumber = teamMemberDetails?.PrimaryContactNumber,
            ContractSigned = teamMemberDetails?.ContractSigned,
            IndemnityInsurance = teamMemberDetails?.IndemnityInsurance,
            LeadDesignerInvolvedInOriginalInstallation = teamMemberDetails?.InvolvedInOriginalInstallation,
            IndemnityInsuranceReason = teamMemberDetails?.IndemnityInsuranceReason,
            LeadDesignerInvolvedRoleReason = teamMemberDetails?.InvolvedRoleReason,
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber
        };
    }
}
