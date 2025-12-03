using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.ProjectTeam;
using HE.Remediation.Core.Data.StoredProcedureResults.MonthlyProgressReport;
using HE.Remediation.Core.Data.StoredProcedureResults.MonthlyProgressReport.ProjectTeam;
using HE.Remediation.Core.Providers.ApplicationDetailsProvider;
using HE.Remediation.Core.Services.PdfRendererService;
using HE.Remediation.Core.Services.RazorRenderer.Models;
using MediatR;
using Syncfusion.Pdf.Graphics;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting;

public class MonthlyReportPdfHandler : IRequestHandler<MonthlyReportPdfRequest, MonthlyReportPdfResponse>
{
    private readonly IApplicationDetailsProvider _applicationDetailsProvider;
    private readonly IMonthlyProgressReportingRepository _monthlyProgressReportingRepository;
    private readonly IProgressReportingProjectTeamRepository _progressReportingProjectTeamRepository;
    private readonly IPdfRenderer _pdfRenderer;

    public MonthlyReportPdfHandler(IApplicationDetailsProvider applicationDetailsProvider,
                                     IMonthlyProgressReportingRepository monthlyProgressReportingRepository, IProgressReportingProjectTeamRepository progressReportingProjectTeamRepository, IPdfRenderer pdfRenderer)
    {
        _applicationDetailsProvider = applicationDetailsProvider;
        _monthlyProgressReportingRepository = monthlyProgressReportingRepository;
        _progressReportingProjectTeamRepository = progressReportingProjectTeamRepository;
        _pdfRenderer = pdfRenderer;
    }

    public async Task<MonthlyReportPdfResponse> Handle(MonthlyReportPdfRequest request, CancellationToken cancellationToken)
    {
        var app = await _applicationDetailsProvider.GetApplicationDetails();
        var mainDetails = await _monthlyProgressReportingRepository.GetProgressReportDetails(request.ProgressReportId);
        var team = mainDetails.TeamHasMembers ? await _progressReportingProjectTeamRepository.GetProjectTeamMembers(new GetTeamMembersParameters() { ApplicationId = app.ApplicationId, ProgressReportId = request.ProgressReportId }) : null;
        var teamGco = mainDetails.TeamHasGco ? await _progressReportingProjectTeamRepository.GetGrantCertifyingOfficer(request.ProgressReportId) : null;
        var teamMembers = team?.Select(t => new ProgressReportDetailsViewModel.TeamMember() { Id = t.Id, Name = t.Name, CompanyName = t.CompanyName, Role = t.RoleName }).ToList();
        var keyRoles = await _progressReportingProjectTeamRepository.GetProjectTeamKeyRolesDetails(request.ProgressReportId);
        var viewModel = MapResponse(app, mainDetails, teamGco, teamMembers, keyRoles);

        var pdf = await _pdfRenderer.RenderPdf(
            Path.Combine("Views", "Pdf", "MonthlyReport.cshtml"),
            viewModel,
            new PdfRenderOptions
            {
                Margins = new PdfMargins
                {
                    All = 40f
                }
            });

        return new MonthlyReportPdfResponse
        {
            File = pdf,
            Filename = $"Progress Report - {mainDetails.DateCreated:MMMM yyyy}.pdf"
        };
    }

    private ProgressReportDetailsViewModel MapResponse(
        ApplicationDetailsModel app,
        GetProgressReportDetailsResult mainDetails,
        GetGrantCertifyingOfficerResult teamGco,
        List<ProgressReportDetailsViewModel.TeamMember> teamMembers,
        GetProjectTeamKeyRolesDetailsResult keyRoles)
    {
        return new ProgressReportDetailsViewModel()
        {
            ApplicationReferenceNumber = app.ApplicationReferenceNumber,
            BuildingName = app.BuildingName,

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
                ? new ProgressReportDetailsViewModel.KeyRoleTeamMember
                {
                    Name = keyRoles.ApplicationLead.Name,
                    CompanyName = keyRoles.ApplicationLead.CompanyName
                }
                : null,
            LeaseholderCommunicator = keyRoles.LeaseholderCommunicator is not null
                ? new ProgressReportDetailsViewModel.KeyRoleTeamMember
                {
                    Name = keyRoles.LeaseholderCommunicator.Name,
                    CompanyName = keyRoles.LeaseholderCommunicator.CompanyName
                }
                : null,
            RegulatoryComplianceMember = keyRoles.RegulatoryComplianceMember is not null
                ? new ProgressReportDetailsViewModel.KeyRoleTeamMember
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

public class MonthlyReportPdfRequest : IRequest<MonthlyReportPdfResponse>
{
    public Guid ProgressReportId { get; set; }
}

public class MonthlyReportPdfResponse
{
    public byte[] File { get; set; }
    public string Filename { get; set; }
}