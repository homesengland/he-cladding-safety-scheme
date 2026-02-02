using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.WorksToCladdingSystems.GetCladdingSystem
{
    public class GetCladdingSystemRequest : IRequest<GetCladdingSystemResponse>
    {
        public GetCladdingSystemRequest()
        {}

        public Guid? FireRiskCladdingSystemsId { get; set; }
    }
}
