using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectSupport
{
    public class GetProjectSupportResponse
    {
        public Guid? Id { get; set; }
        public Guid ApplicationId { get; set; }
        public string BuildingName { get; set; }
        public string ApplicationReferenceNumber { get; set; }
        public ENoYes? RequiresSupport { get; set; }
        public ETaskStatus? TaskStatusId { get; set; }
    }
}