using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.Application.TaskList.GetTaskList
{
    public class GetTaskListResponse
    {
        public Guid ApplicationId { get; set; }
        public string ApplicationReferenceNumber { get; set; }
        public EApplicationStatus ApplicationStatusId { get; set; }
        public ETaskStatus ApplicationLeaseHolderEngagementStatusId { get; set; }
        public ETaskStatus ApplicationBuildingDetailsStatusId { get; set; }
        public ETaskStatus ApplicationResponsibleEntityStatusId { get; set; }
        public ETaskStatus ApplicationFundingRoutesStatusId { get; set; }
        public ETaskStatus ApplicationBankDetailsStatusId { get; set; }
        public ETaskStatus ConfirmDeclarationStatusId { get; set; }
        public ETaskStatus ApplicationFraStatusId { get; set; }
        public ETaskStatus ApplicationFireRiskAssessmentStatusId { get; set; }
        public EFraBuildingWorkType? FraBuildingWorkType { get; set; }
        public bool IsDataIngestion { get; set; }
    }
}
