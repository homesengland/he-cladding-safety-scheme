using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.Application
{
    public class TaskListViewModel
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
        public ETaskStatus ApplicationFireRiskAssessmentStatusId { get; set; }

        public bool Phase2ReadyForDeclaration()
        {
            return ApplicationLeaseHolderEngagementStatusId == ETaskStatus.Completed
                   && ApplicationBuildingDetailsStatusId == ETaskStatus.Completed
                   && ApplicationResponsibleEntityStatusId == ETaskStatus.Completed
                   && ApplicationFundingRoutesStatusId == ETaskStatus.Completed
                   && ApplicationBankDetailsStatusId == ETaskStatus.Completed;
        }

        public bool Phase2Completed()
        {
            return ApplicationBuildingDetailsStatusId == ETaskStatus.Completed
                   && ApplicationResponsibleEntityStatusId == ETaskStatus.Completed
                   && ApplicationFundingRoutesStatusId == ETaskStatus.Completed
                   && ApplicationBankDetailsStatusId == ETaskStatus.Completed
                   && ConfirmDeclarationStatusId == ETaskStatus.Completed;
        }
        public bool Phase3Completed()
        {
            return ApplicationFireRiskAssessmentStatusId == ETaskStatus.Completed;
        }

        public bool Phase4Completed()
        {
            return ApplicationStatusId != EApplicationStatus.NotStarted && ApplicationStatusId != EApplicationStatus.InProgress;
        }
    }
} 