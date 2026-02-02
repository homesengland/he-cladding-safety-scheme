using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Providers.ApplicationDetailsProvider;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectTeam.GrantCertifyingOfficer;

public class GetGrantCertifyingOfficerCheckYourAnswersHandler : IRequestHandler<GetGrantCertifyingOfficerCheckYourAnswersRequest, GetGrantCertifyingOfficerCheckYourAnswersResponse>
{
    private readonly IApplicationDetailsProvider _detailsProvider;
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IProgressReportingProjectTeamRepository _progressReportingProjectTeamRepository;

    public GetGrantCertifyingOfficerCheckYourAnswersHandler(
        IApplicationDetailsProvider detailsProvider,
        IApplicationDataProvider applicationDataProvider,
        IProgressReportingProjectTeamRepository progressReportingProjectTeamRepository)
    {
        _detailsProvider = detailsProvider;
        _applicationDataProvider = applicationDataProvider;
        _progressReportingProjectTeamRepository = progressReportingProjectTeamRepository;
    }

    public async ValueTask<GetGrantCertifyingOfficerCheckYourAnswersResponse> Handle(GetGrantCertifyingOfficerCheckYourAnswersRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var details = await _detailsProvider.GetApplicationDetails();
        var progressReportId = _applicationDataProvider.GetProgressReportId();

        var gcoDetails = await _progressReportingProjectTeamRepository.GetGrantCertifyingOfficer(progressReportId);

        return new GetGrantCertifyingOfficerCheckYourAnswersResponse
        {
            ApplicationReferenceNumber = details.ApplicationReferenceNumber,
            ApplicationBuildingName = details.BuildingName,
            HasGco = gcoDetails.HasGco ?? false,
            TeamMemberId = gcoDetails.TeamMemberId,
            CompanyName = gcoDetails.CompanyName,
            TeamMemberName = gcoDetails.TeamMemberName,
            TeamMemberRole = gcoDetails.TeamMemberRole,

            NameNumber = gcoDetails.NameNumber,
            AddressLine1 = gcoDetails.AddressLine1,
            AddressLine2 = gcoDetails.AddressLine2,
            City = gcoDetails.City,
            County = gcoDetails.County,
            Postcode = gcoDetails.Postcode,

            CompanyRegistrationNumber = gcoDetails.CompanyRegistrationNumber,
            EmailAddress = gcoDetails.EmailAddress,
            PrimaryContactNumber = gcoDetails.PrimaryContactNumber,
            ContractSigned = gcoDetails.ContractStartDate != null,

            IndemnityInsurance = gcoDetails.IndemnityInsurance,
            IndemnityInsuranceReason = gcoDetails.IndemnityInsuranceReason,
            InvolvedInOriginalInstallation = gcoDetails.InvolvedInOriginalInstallation,
            InvolvedRoleReason = gcoDetails.InvolvedRoleReason,

            AuthorisedSignatory = gcoDetails.AuthorisedSignatory,
            AuthorisedSignatoryEmailAddress = gcoDetails.AuthorisedSignatoryEmailAddress,
            AuthorisedSignatoryDateAppointed = gcoDetails.AuthorisedSignatoryDateAppointed,
            ContractStartDate = gcoDetails.ContractStartDate
        };
    }
}

public class GetGrantCertifyingOfficerCheckYourAnswersRequest : IRequest<GetGrantCertifyingOfficerCheckYourAnswersResponse>
{
}

public class GetGrantCertifyingOfficerCheckYourAnswersResponse
{
    public string ApplicationReferenceNumber { get; set; }
    public string ApplicationBuildingName { get; set; }

    public Guid Id { get; set; }
    public bool HasGco { get; set; }
    public Guid? TeamMemberId { get; set; }
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
    public Guid? TeamMemberRoleId { get; set; }
    public string NameNumber { get; set; }
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