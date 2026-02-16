using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Providers.ApplicationDetailsProvider;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectTeam.GrantCertifyingOfficer;

public class GetCheckYourUpdatedGcoHandler : IRequestHandler<GetCheckYourUpdatedGcoRequest, GetCheckYourUpdatedGcoResponse>
{
    private readonly IApplicationDetailsProvider _detailsProvider;
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IProgressReportingProjectTeamRepository _progressReportingProjectTeamRepository;

    public GetCheckYourUpdatedGcoHandler(
        IApplicationDetailsProvider detailsProvider,
        IApplicationDataProvider applicationDataProvider,
        IProgressReportingProjectTeamRepository progressReportingProjectTeamRepository)
    {
        _detailsProvider = detailsProvider;
        _applicationDataProvider = applicationDataProvider;
        _progressReportingProjectTeamRepository = progressReportingProjectTeamRepository;
    }

    public async ValueTask<GetCheckYourUpdatedGcoResponse> Handle(GetCheckYourUpdatedGcoRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var details = await _detailsProvider.GetApplicationDetails();
        var progressReportId = _applicationDataProvider.GetProgressReportId();

        var gcoDetails = await _progressReportingProjectTeamRepository.GetGrantCertifyingOfficer(progressReportId);

        return new GetCheckYourUpdatedGcoResponse
        {
            ApplicationReferenceNumber = details.ApplicationReferenceNumber,
            ApplicationBuildingName = details.BuildingName,
            TeamMemberId = gcoDetails.TeamMemberId.Value,
            CompanyName = gcoDetails.CompanyName,
            TeamMemberName = gcoDetails.TeamMemberName,
            TeamMemberRole = gcoDetails.TeamMemberRole,
            CompanyRegistrationNumber = gcoDetails.CompanyRegistrationNumber,
            EmailAddress = gcoDetails.EmailAddress,
            PrimaryContactNumber = gcoDetails.PrimaryContactNumber,
            ContractSigned = gcoDetails.ContractStartDate != null,
            IndemnityInsurance = gcoDetails.IndemnityInsurance,
            IndemnityInsuranceReason = gcoDetails.IndemnityInsuranceReason,
            InvolvedInOriginalInstallation = gcoDetails.InvolvedInOriginalInstallation,
            InvolvedRoleReason = gcoDetails.InvolvedRoleReason,
        };
    }
}

public class GetCheckYourUpdatedGcoRequest : IRequest<GetCheckYourUpdatedGcoResponse>
{
}

public class GetCheckYourUpdatedGcoResponse
{
    public string ApplicationReferenceNumber { get; set; }
    public string ApplicationBuildingName { get; set; }

    public Guid Id { get; set; }
    public bool HasGco { get; set; }
    public Guid TeamMemberId { get; set; }
    public string TeamMemberName { get; set; }
    public string CompanyName { get; set; }
    public string CompanyRegistrationNumber { get; set; }
    public string EmailAddress { get; set; }
    public string PrimaryContactNumber { get; set; }
    public bool IndemnityInsurance { get; set; }
    public string IndemnityInsuranceReason { get; set; }
    public bool InvolvedInOriginalInstallation { get; set; }
    public string InvolvedRoleReason { get; set; }
    public string TeamMemberRole { get; set; }
    public Guid TeamMemberRoleId { get; set; }
    public string BuildingName { get; set; }
    public string BuildingNumber { get; set; }
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string City { get; set; }
    public string County { get; set; }
    public string Postcode { get; set; }
    public string AuthorisedSignatory { get; set; }
    public string AuthorisedSignatoryEmailAddress { get; set; }
    public DateTime? AuthorisedSignatoryDateAppointed { get; set; }
    public DateTime? ContractStartDate { get; set; }

    public bool ContractSigned { get; set; } // ????
}