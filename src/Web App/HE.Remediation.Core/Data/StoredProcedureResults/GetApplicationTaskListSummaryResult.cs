using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.StoredProcedureResults;

public class GetApplicationTaskListSummaryResult
{
    public string ApplicationReferenceNumber { get; set; }
    public EApplicationStatus ApplicationStatusId { get; set; }
    public ETaskStatus ApplicationLeaseHolderEngagementStatusId { get; set; }
    public ETaskStatus ApplicationBuildingDetailsStatusId { get; set; }
    public ETaskStatus ApplicationResponsibleEntityStatusId { get; set; }
    public ETaskStatus ApplicationFundingRoutesStatusId { get; set; }
    public ETaskStatus ApplicationBankDetailsStatusId { get; set; }
    public bool ConfirmDeclaration { get; set; }
    public ETaskStatus ApplicationFraStatusId { get; set; }
    public ETaskStatus ApplicationFireRiskAssessmentStatusId { get; set; }
    public EFraBuildingWorkType? FraBuildingWorkTypeId { get; set; }
    public bool IsDataIngestion { get; set; }
}