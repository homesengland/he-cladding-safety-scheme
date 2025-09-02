using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.Application
{
    public class TaskListViewModel
    {
        public Guid ApplicationId { get; set; }
        public EApplicationScheme ApplicationScheme { get; set; }
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
        public bool IsDataIngestion { get; set; }

        public bool Phase2ReadyForDeclaration()
        {
            return ApplicationLeaseHolderEngagementStatusId == ETaskStatus.Completed
                   && ApplicationBuildingDetailsStatusId == ETaskStatus.Completed
                   && ApplicationResponsibleEntityStatusId == ETaskStatus.Completed
                   && ApplicationFundingRoutesStatusId == ETaskStatus.Completed
                   && ((IsCladdingSafetyScheme() && ApplicationBankDetailsStatusId == ETaskStatus.Completed) || (!IsCladdingSafetyScheme()));
        }

        public bool Phase2Completed()
        {
            return ApplicationBuildingDetailsStatusId == ETaskStatus.Completed
                   && ApplicationResponsibleEntityStatusId == ETaskStatus.Completed
                   && ApplicationFundingRoutesStatusId == ETaskStatus.Completed
                   && ((IsCladdingSafetyScheme() && ApplicationBankDetailsStatusId == ETaskStatus.Completed) || (!IsCladdingSafetyScheme()))
                   && ConfirmDeclarationStatusId == ETaskStatus.Completed;
        }
        public bool Phase3Completed()
        {
            return ApplicationFireRiskAssessmentStatusId == ETaskStatus.Completed && ApplicationFraStatusId == ETaskStatus.Completed;
        }

        public bool Phase4Completed()
        {
            return ApplicationStatusId != EApplicationStatus.ApplicationNotStarted && ApplicationStatusId != EApplicationStatus.ApplicationInProgress;
        }

        public bool IsBankAccountInformationRequired()
        {
            return ApplicationScheme != EApplicationScheme.ResponsibleActorsScheme
                && ApplicationScheme != EApplicationScheme.SocialSector
                && ApplicationScheme != EApplicationScheme.SelfRemediating;
        }

        public bool IsCladdingSafetyScheme()
        {
            return ApplicationScheme == EApplicationScheme.CladdingSafetyScheme;
        }
    }
} 