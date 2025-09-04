using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorksPackageCladdingSystem.ResetCladdingSystem
{
    public class CladdingSystemChangeAnswersRequest : IRequest
    {
        public Guid FireRiskCladdingSystemsId { get; set; }

        public int CladdingSystemIndex { get; set; }
    }
}
