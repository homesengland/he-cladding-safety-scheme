using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.ProjectTeam;
using HE.Remediation.Core.Data.StoredProcedureResults.MonthlyProgressReport;
using HE.Remediation.Core.Data.StoredProcedureResults.MonthlyProgressReport.ProjectTeam;
using HE.Remediation.Core.Providers.ApplicationDetailsProvider;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting;

public class GetProgressReportDetailsHandler : IRequestHandler<GetProgressReportDetailsRequest, GetProgressReportDetailsResponse>
{
    private readonly IApplicationDetailsProvider _applicationDetailsProvider;
    private readonly IMonthlyProgressReportingRepository _monthlyProgressReportingRepository;
    private readonly IProgressReportingProjectTeamRepository _progressReportingProjectTeamRepository;

    public GetProgressReportDetailsHandler(IApplicationDetailsProvider applicationDetailsProvider,
                                     IMonthlyProgressReportingRepository monthlyProgressReportingRepository, IProgressReportingProjectTeamRepository progressReportingProjectTeamRepository)
    {
        _applicationDetailsProvider = applicationDetailsProvider;
        _monthlyProgressReportingRepository = monthlyProgressReportingRepository;
        _progressReportingProjectTeamRepository = progressReportingProjectTeamRepository;
    }

    public async ValueTask<GetProgressReportDetailsResponse> Handle(GetProgressReportDetailsRequest request, CancellationToken cancellationToken)
    {
        var app = await _applicationDetailsProvider.GetApplicationDetails();
        var mainDetails = await _monthlyProgressReportingRepository.GetProgressReportDetails(request.ProgressReportId);
        var team = mainDetails.TeamHasMembers ? await _progressReportingProjectTeamRepository.GetProjectTeamMembers(new GetTeamMembersParameters() { ApplicationId = app.ApplicationId, ProgressReportId = request.ProgressReportId }) : null;
        var teamGco = mainDetails.TeamHasGco ? await _progressReportingProjectTeamRepository.GetGrantCertifyingOfficer(request.ProgressReportId) : null;
        var teamMembers = team?.Select(t => new GetProgressReportDetailsResponse.TeamMember() { Id = t.Id, Name = t.Name, CompanyName = t.CompanyName, Role = t.RoleName }).ToList();
        var keyRoles = await _progressReportingProjectTeamRepository.GetProjectTeamKeyRolesDetails(request.ProgressReportId);
        return MapResponse(app, mainDetails, teamGco, teamMembers, keyRoles);
    }

    private static GetProgressReportDetailsResponse MapResponse(
        ApplicationDetailsModel app, 
        GetProgressReportDetailsResult mainDetails, 
        GetGrantCertifyingOfficerResult teamGco, 
        List<GetProgressReportDetailsResponse.TeamMember> teamMembers,
        GetProjectTeamKeyRolesDetailsResult keyRoles)
    {
        return new GetProgressReportDetailsResponse
        {
            ApplicationReferenceNumber = app.ApplicationReferenceNumber,
            BuildingName = app.BuildingName,
            ApplicationSchemeId = mainDetails.ApplicationSchemeId,

            Id = mainDetails.Id,
            DateCreated = mainDetails.DateCreated,
            DateDue = mainDetails.DateDue,
            DateSubmitted = mainDetails.DateSubmitted,

            // KD - Works Planning
            ExpectedTenderDate = mainDetails.ExpectedTenderDate,
            ActualTenderDate = mainDetails.ActualTenderDate,
            ExpectedLeadContractorAppointmentDate = mainDetails.ExpectedLeadContractorAppointmentDate,
            ActualLeadContractAppointmentDate = mainDetails.ActualLeadContractAppointmentDate,
            ExpectedWorksPackageSubmissionDate = mainDetails.ExpectedWorksPackageSubmissionDate,
            ContractorTenderTypeId = mainDetails.ContractorTenderTypeId,
            ContractorTenderReason = mainDetails.ContractorTenderReason,
            WorksPlanningChangeTypeId = mainDetails.WorksPlanningChangeTypeId,
            WorksPlanningChangeReason = mainDetails.WorksPlanningChangeReason,

            // KD - Building Control
            BuildingControlExpectedApplicationDate = mainDetails.BuildingControlExpectedApplicationDate,
            BuildingControlActualApplicationDate = mainDetails.BuildingControlActualApplicationDate,
            BuildingControlGateway2Reference = mainDetails.BuildingControlGateway2Reference,
            BuildingControlValidationDate = mainDetails.BuildingControlValidationDate,
            BuildingControlDecisionDate = mainDetails.BuildingControlDecisionDate,
            BuildingControlDecisionTypeId = mainDetails.BuildingControlDecisionTypeId,
            BuildingControlDecisionType = mainDetails.BuildingControlDecisionType,
            BuildingControlDecisionDocuments = [.. mainDetails.BuildingControlDecisionDocumentsCsv?.Split(",") ?? []],
            BuildingControlChangeTypeId = mainDetails.BuildingControlChangeTypeId,
            BuildingControlChangeReason = mainDetails.BuildingControlChangeReason,

            // KD - Planning Permission
            WorksNeedPlanningPermission = mainDetails.WorksNeedPlanningPermission,
            HaveAppliedPlanningPermission = mainDetails.HaveAppliedPlanningPermission,
            PlanningPermissionDateSubmitted = mainDetails.PlanningPermissionDateSubmitted,
            PlanningPermissionDateApproved = mainDetails.PlanningPermissionDateApproved,
            PlanningPermissionPlanToSubmitDate = mainDetails.PlanningPermissionPlanToSubmitDate,
            PlanningPermissionReasonNotApplied = mainDetails.PlanningPermissionReasonNotApplied,
            PlanningPermissionChangeTypeId = mainDetails.PlanningPermissionChangeTypeId,
            PlanningPermissionChangeReason = mainDetails.PlanningPermissionChangeReason,
            
            // KD - Remediation
            RemediationFullCompletionOfWorksDate = mainDetails.RemediationFullCompletionOfWorksDate,
            RemediationPracticalCompletionDate = mainDetails.RemediationPracticalCompletionDate,
            RemediationChangeTypeId = mainDetails.RemediationChangeTypeId,
            RemediationChangeReason = mainDetails.RemediationChangeReason,

            // Project Plan
            IntentToProceedType = mainDetails.IntentToProceedType,
            InternalAdditionalWork = mainDetails.InternalAdditionalWork,
            RemainingAmount = mainDetails.RemainingAmount,
            EnoughFunds = mainDetails.EnoughFunds,
            ProjectPlanDocuments = [.. mainDetails.ProjectPlanDocumentsCsv?.Split(",") ?? []],
            PtsUpliftDocuments = [.. mainDetails.PtsUpliftDocumentsCsv?.Split(",") ?? []],

            // Leaseholder Communication
            HasContacted = mainDetails.HasContacted,
            LastCommunicationDate = mainDetails.LastCommunicationDate,
            LeaseholderCommunicationDocuments = [.. mainDetails.LeaseholderCommunicationDocumentsCsv?.Split(",") ?? []],

            // Support
            RequiresSupport = mainDetails.RequiresSupport,
            LeadDesignerNeedsSupport = mainDetails.LeadDesignerNeedsSupport,
            OtherMembersNeedsSupport = mainDetails.OtherMembersNeedsSupport,
            QuotesNeedsSupport = mainDetails.QuotesNeedsSupport,
            PlanningPermissionNeedsSupport = mainDetails.PlanningPermissionNeedsSupport,
            OtherNeedsSupport = mainDetails.OtherNeedsSupport,
            SupportNeededReason = mainDetails.SupportNeededReason,

            // Team Members
            TeamMembers = teamMembers ?? [],

            GcoName = teamGco?.TeamMemberName,
            GcoCompanyName = teamGco?.CompanyName,
            GcoNameNumber = teamGco?.NameNumber,
            GcoAddressLine1 = teamGco?.AddressLine1,
            GcoAddressLine2 = teamGco?.AddressLine2,
            GcoCity = teamGco?.City,
            GcoCounty = teamGco?.County,
            GcoPostCode = teamGco?.Postcode,
            GcoAuthorisedSignatory = teamGco?.AuthorisedSignatory,
            GcoAuthorisedSignatoryEmailAddress = teamGco?.AuthorisedSignatoryEmailAddress,
            GcoContractStartDate = teamGco?.AuthorisedSignatoryDateAppointed,

            ApplicationLead = keyRoles.ApplicationLead is not null 
                ? new GetProgressReportDetailsResponse.KeyRoleTeamMember
                {
                    Name = keyRoles.ApplicationLead.Name,
                    CompanyName = keyRoles.ApplicationLead.CompanyName
                }
                : null,
            LeaseholderCommunicator = keyRoles.LeaseholderCommunicator is not null
                ? new GetProgressReportDetailsResponse.KeyRoleTeamMember
                {
                    Name = keyRoles.LeaseholderCommunicator.Name,
                    CompanyName = keyRoles.LeaseholderCommunicator.CompanyName
                }
                : null,
            RegulatoryComplianceMember = keyRoles.RegulatoryComplianceMember is not null
                ? new GetProgressReportDetailsResponse.KeyRoleTeamMember
                {
                    Name = keyRoles.RegulatoryComplianceMember.Name,
                    CompanyName = keyRoles.RegulatoryComplianceMember.CompanyName
                }
                : null,

            // Legacy Data
            HasLegacyReport = mainDetails.HasLegacyReport,
            HasBuildingSafetyRegulatorRegistrationCode = mainDetails.HasBuildingSafetyRegulatorRegistrationCode,
            BuildingSafetyRegulatorRegistrationCode = mainDetails.BuildingSafetyRegulatorRegistrationCode,
            Is7StoreysOr18Metres = mainDetails.Is7StoreysOr18Metres,
            HasAppliedForBuildingControl = mainDetails.HasAppliedForBuildingControl,
            HasProjectPlanMilestones = mainDetails.HasProjectPlanMilestones,
            HasSoughtQuotesOrIssuedTender = mainDetails.HasSoughtQuotesOrIssuedTender,
            HasAppointedTeamMembers = mainDetails.HasAppointedTeamMembers,
            BuildingControlActualSubmissionInformation = mainDetails.BuildingControlActualSubmissionInformation,
            BuildingControlValidationInformation = mainDetails.BuildingControlValidationInformation,
            BuildingControlDecisionInformation = mainDetails.BuildingControlDecisionInformation,
            RisksAndBlockersSummary = mainDetails.RisksAndBlockersSummary
        };
    }
}
