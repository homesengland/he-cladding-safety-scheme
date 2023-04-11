using HE.Remediation.Core.Data.StoredProcedureResults;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.AssessorList.GetAssessorList
{
    public class GetAssessorListResponse
    {
        public Guid ApplicationId { get; set; }
        public string ApplicationReferenceNumber { get; set; }
        public IEnumerable<GetFireRiskAssessorListResult> AssessorList { get; set; }
    }
}
