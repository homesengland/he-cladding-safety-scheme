using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCladdingSystem.CladdingSystemCheckYourAnswers.Get;

public class GetCladdingSystemCheckYourAnswersRequest : IRequest<GetCladdingSystemCheckYourAnswersResponse>
{
    public Guid FireRiskCladdingSystemsId { get; set; }

    public int CladdingSystemIndex { get; set; }
}
