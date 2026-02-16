using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting.KeyDates;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Providers.ApplicationDetailsProvider;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.KeyDates;
public class GetKeyDatesHandler : KeyDatesEntryPoint, IRequestHandler<GetKeyDatesRequest, GetKeyDatesResponse>
{
    private readonly IProgressReportingKeyDatesRepository _progressReportingKeyDatesRepository;
    private readonly IApplicationDetailsProvider _detailsProvider;

    public GetKeyDatesHandler(
        IApplicationDataProvider dataProvider, 
        IApplicationDetailsProvider detailsProvider, 
        IProgressReportingKeyDatesRepository progressReportingKeyDatesRepository, 
        IMonthlyProgressReportingRepository monthlyProgressReportingRepository) 
        : base(dataProvider, progressReportingKeyDatesRepository, monthlyProgressReportingRepository)
    {
        _detailsProvider = detailsProvider;
        _progressReportingKeyDatesRepository = progressReportingKeyDatesRepository;
    }

    public async ValueTask<GetKeyDatesResponse> Handle(GetKeyDatesRequest request, CancellationToken cancellationToken)
    {
        var details = await _detailsProvider.GetApplicationDetails();
        var result = await _progressReportingKeyDatesRepository.GetProgressReportKeyDates(request.MonthlyProgressReportId);

        await SetToInProgress();

        if (result == null)
        {
            return new GetKeyDatesResponse();
        }

        return new GetKeyDatesResponse
        {
            ApplicationReferenceNumber = details.ApplicationReferenceNumber,
            BuildingName = details.BuildingName,

            ExpectedTenderDate = result.ExpectedTenderDate,
            ExpectedLeadContractorAppointmentDate = result.ExpectedLeadContractorAppointmentDate,
            ActualTenderDate = result.ActualTenderDate,
            ActualLeadContractAppointmentDate = result.ActualLeadContractAppointmentDate,
            ExpectedWorksPackageSubmissionDate = result.ExpectedWorksPackageSubmissionDate,
            WorksPlanningChangeType = result.WorksPlanningChangeType,
            WorksPlanningChangeReason = result.WorksPlanningChangeReason,
            ContractorTenderTypeId = result.ContractorTenderTypeId,
            ContractorTenderReason = result.ContractorTenderReason,

            PreviousExpectedTenderDate = result.PreviousExpectedTenderDate,
            PreviousExpectedLeadContractorAppointmentDate = result.PreviousExpectedLeadContractorAppointmentDate,
            PreviousActualTenderDate = result.PreviousActualTenderDate,
            PreviousActualLeadContractAppointmentDate = result.PreviousActualLeadContractorAppointmentDate,
            PreviousExpectedWorksPackageSubmissionDate = result.PreviousExpectedWorksPackageSubmissionDate,

            BuildingControlExpectedApplicationDate = result.BuildingControlExpectedApplicationDate,
            BuildingControlActualApplicationDate = result.BuildingControlActualApplicationDate,
            BuildingControlGateway2Reference = result.BuildingControlGateway2Reference,
            BuildingControlValidationDate = result.BuildingControlValidationDate,
            BuildingControlDecisionDate = result.BuildingControlDecisionDate,
            BuildingControlDecision = result.BuildingControlDecision,
            BuildingControlDecisionFiles = result.BuildingControlDecisionFiles.Select(x => x.Name).ToList(),
            BuildingControlChangeType = result.BuildingControlChangeType,
            BuildingControlChangeReason = result.BuildingControlChangeReason,

            PreviousBuildingControlExpectedApplicationDate = result.PreviousBuildingControlExpectedApplicationDate,
            PreviousBuildingControlActualApplicationDate = result.PreviousBuildingControlActualApplicationDate,
            PreviousBuildingControlValidationDate = result.PreviousBuildingControlValidationDate,
            PreviousBuildingControlDecisionDate = result.PreviousBuildingControlDecisionDate,

            WorksNeedPlanningPermission = result.WorksNeedPlanningPermission,
            HaveAppliedPlanningPermission = result.HaveAppliedPlanningPermission,
            PlanningPermissionDateSubmitted = result.PlanningPermissionDateSubmitted,
            PlanningPermissionDateApproved = result.PlanningPermissionDateApproved,
            PlanningPermissionReasonNotApplied = result.PlanningPermissionReasonNotApplied,
            PlanningPermissionPlanToSubmitDate = result.PlanningPermissionPlanToSubmitDate,
            PlanningPermissionChangeType = result.PlanningPermissionChangeType,
            PlanningPermissionChangeReason = result.PlanningPermissionChangeReason,

            PreviousPlanningPermissionDateSubmitted = result.PreviousPlanningPermissionDateSubmitted,
            PreviousPlanningPermissionDateApproved = result.PreviousPlanningPermissionDateApproved,
            PreviousPlanningPermissionPlanToSubmitDate = result.PreviousPlanningPermissionPlanToSubmitDate,

            RemediationFullCompletionOfWorksDate = result.RemediationFullCompletionOfWorksDate,
            RemediationPracticalCompletionDate = result.RemediationPracticalCompletionDate,
            RemediationChangeType = result.RemediationChangeType,
            RemediationChangeReason = result.RemediationChangeReason,

            PreviousRemediationFullCompletionOfWorksDate = result.PreviousRemediationFullCompletionOfWorksDate,
            PreviousRemediationPracticalCompletionDate = result.PreviousRemediationPracticalCompletionDate
        };
    }
}

public class GetKeyDatesRequest(Guid monthlyProgressReportId) : IRequest<GetKeyDatesResponse>
{
    public Guid MonthlyProgressReportId { get; set; } = monthlyProgressReportId;
}

public class GetKeyDatesResponse
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }

    // Works Planning
    public DateTime? ExpectedTenderDate { get; set; }
    public DateTime? ExpectedLeadContractorAppointmentDate { get; set; }
    public DateTime? ActualTenderDate { get; set; }
    public DateTime? ActualLeadContractAppointmentDate { get; set; }
    public DateTime? ExpectedWorksPackageSubmissionDate { get; set; }
    public string WorksPlanningChangeType { get; set; }
    public string WorksPlanningChangeReason { get; set; }

    public EContractorTenderType? ContractorTenderTypeId { get; set; }
    public string ContractorTenderReason { get; set; }

    public DateTime? PreviousExpectedTenderDate { get; set; }
    public DateTime? PreviousExpectedLeadContractorAppointmentDate { get; set; }
    public DateTime? PreviousActualTenderDate { get; set; }
    public DateTime? PreviousActualLeadContractAppointmentDate { get; set; }
    public DateTime? PreviousExpectedWorksPackageSubmissionDate { get; set; }


    // Building Control
    public DateTime? BuildingControlExpectedApplicationDate { get; set; }
    public DateTime? BuildingControlActualApplicationDate { get; set; }
    public string BuildingControlGateway2Reference { get; set; }
    public DateTime? BuildingControlValidationDate { get; set; }
    public DateTime? BuildingControlDecisionDate { get; set; }
    public EBuildingControlDecisionType? BuildingControlDecision { get; set; }
    public IList<string> BuildingControlDecisionFiles { get; set; } = new List<string>();
    public string BuildingControlChangeType { get; set; }
    public string BuildingControlChangeReason { get; set; }

    public DateTime? PreviousBuildingControlExpectedApplicationDate { get; set; }
    public DateTime? PreviousBuildingControlActualApplicationDate { get; set; }
    public DateTime? PreviousBuildingControlValidationDate { get; set; }
    public DateTime? PreviousBuildingControlDecisionDate { get; set; }

    // Planning Permission
    public int? WorksNeedPlanningPermission { get; set; }
    public bool? HaveAppliedPlanningPermission { get; set; }
    public DateTime? PlanningPermissionDateSubmitted { get; set; }
    public DateTime? PlanningPermissionDateApproved { get; set; }
    public string PlanningPermissionReasonNotApplied { get; set; }
    public DateTime? PlanningPermissionPlanToSubmitDate { get; set; }
    public string PlanningPermissionChangeType { get; set; }
    public string PlanningPermissionChangeReason { get; set; }

    public DateTime? PreviousPlanningPermissionDateSubmitted { get; set; }
    public DateTime? PreviousPlanningPermissionDateApproved { get; set; }
    public DateTime? PreviousPlanningPermissionPlanToSubmitDate { get; set; }

    // Remediation
    public DateTime? RemediationFullCompletionOfWorksDate { get; set; }
    public DateTime? RemediationPracticalCompletionDate { get; set; }
    public string RemediationChangeType { get; set; }
    public string RemediationChangeReason { get; set; }

    public DateTime? PreviousRemediationFullCompletionOfWorksDate { get; set; }
    public DateTime? PreviousRemediationPracticalCompletionDate { get; set; }
}