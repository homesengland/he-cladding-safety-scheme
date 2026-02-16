using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.Details.GetProgressReportCompanyDetails;

public class GetProgressReportCompanyDetailsHandler : IRequestHandler<GetProgressReportCompanyDetailsRequest, GetProgressReportCompanyDetailsResponse>
{
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IProgressReportingRepository _progressReportingRepository;
    private readonly IApplicationDataProvider _applicationDataProvider;

    public GetProgressReportCompanyDetailsHandler(
        IApplicationRepository applicationRepository, 
        IBuildingDetailsRepository buildingDetailsRepository, 
        IProgressReportingRepository progressReportingRepository, 
        IApplicationDataProvider applicationDataProvider)
    {
        _applicationRepository = applicationRepository;
        _buildingDetailsRepository = buildingDetailsRepository;
        _progressReportingRepository = progressReportingRepository;
        _applicationDataProvider = applicationDataProvider;
    }

    public async ValueTask<GetProgressReportCompanyDetailsResponse> Handle(GetProgressReportCompanyDetailsRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        var reference = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var teamMember = await _progressReportingRepository.GetTeamMember(request.TeamMemberId);
        var submittedDate = await _progressReportingRepository.GetProgressReportDateSubmitted();

        return new GetProgressReportCompanyDetailsResponse
        {
            ApplicationReferenceNumber = reference,
            BuildingName = buildingName,
            CompanyName = teamMember.CompanyName,
            CompanyRegistration = teamMember.CompanyRegistration,
            ConsiderateConstructorSchemeType = teamMember.ConsiderateConstructorSchemeType,
            ContractSigned = teamMember.ContractSigned,
            EmailAddress = teamMember.EmailAddress,
            IndemnityInsurance = teamMember.IndemnityInsurance,
            IndemnityInsuranceReason = teamMember.IndemnityInsuranceReason,
            InvolvedInOriginalInstallation = teamMember.InvolvedInOriginalInstallation,
            InvolvedRoleReason = teamMember.InvolvedRoleReason,
            Name = teamMember.Name,
            OtherRole = teamMember.OtherRole,
            PrimaryContactNumber = teamMember.PrimaryContactNumber,
            Role = teamMember.Role!.Value,
            SubmittedDate = submittedDate,
            TeamMemberId = teamMember.TeamMemberId!.Value,
            ProgressReportId = _applicationDataProvider.GetProgressReportId()
        };
    }
}