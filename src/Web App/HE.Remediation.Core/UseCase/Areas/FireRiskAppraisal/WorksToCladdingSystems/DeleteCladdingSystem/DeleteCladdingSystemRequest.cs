using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.WorksToCladdingSystems.DeleteCladdingSystem
{
    public class DeleteCladdingSystemRequest : IRequest<Unit>
    {
        public DeleteCladdingSystemRequest()
        {}

        public Guid FireRiskCladdingSystemsId { get; set; }
    }
}
