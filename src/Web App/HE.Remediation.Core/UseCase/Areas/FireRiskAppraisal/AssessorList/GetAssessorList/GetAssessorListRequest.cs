using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.AssessorList.GetAssessorList
{
    public class GetAssessorListRequest : IRequest<GetAssessorListResponse>
    {
        internal GetAssessorListRequest()
        {
                
        }

        public static GetAssessorListRequest Request => new();
    }
}
