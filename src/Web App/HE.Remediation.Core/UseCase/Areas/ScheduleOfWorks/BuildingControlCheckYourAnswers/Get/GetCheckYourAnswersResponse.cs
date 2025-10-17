using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Data.StoredProcedureResults;

namespace HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.BuildingControlCheckYourAnswers.Get
{
    public class GetCheckYourAnswersResponse
    {
        public string BuildingName { get; set; }
        public string ApplicationReferenceNumber { get; set; }
        public bool IsSubmitted { get; set; }
        public ENoYes? IsBuildingControlApprovalApplied { get; set; }
        public DateTime? BuildingControlApprovalDate { get; set; }
        public ETaskStatus BuildingControlStatusId { get; set; }
        public List<FileResult> AddedFiles { get; set; }
    }
}