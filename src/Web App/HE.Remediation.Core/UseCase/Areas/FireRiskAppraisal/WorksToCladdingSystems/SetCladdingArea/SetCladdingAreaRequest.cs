using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.WorksToCladdingSystems.SetCladdingArea
{
    public class SetCladdingAreaRequest : IRequest<Unit>
    {
        public int? RecommendedCladdingArea { get; set; }
    }
}
