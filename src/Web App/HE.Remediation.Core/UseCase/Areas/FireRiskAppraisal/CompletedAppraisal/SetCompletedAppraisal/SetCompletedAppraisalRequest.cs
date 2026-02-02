using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.CompletedAppraisal.SetCompletedAppraisal
{
    public class SetCompletedAppraisalRequest : IRequest<Unit>
    {
        public bool? IsAppraisalCompleted { get; set; }
    }
}
