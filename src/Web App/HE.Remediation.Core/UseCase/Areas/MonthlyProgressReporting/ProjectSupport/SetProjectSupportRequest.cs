using HE.Remediation.Core.Enums;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectSupport
{
    public class SetProjectSupportRequest : IRequest
    {
        public Guid ProgressReportId { get; set; }
        public Guid ApplicationId { get; set; }
        public Guid? Id { get; set; }
        public string BuildingName { get; set; }
        public string ApplicationReferenceNumber { get; set; }
        public ENoYes? RequiresSupport { get; set; }
        public ESubmitAction SubmitAction { get; set; }
    }
}