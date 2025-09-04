using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCladdingSystem.CladdingSystemCheckYourAnswers.Set;

public class SetCladdingSystemCheckYourAnswersRequest : IRequest<Unit>
{
    public Guid FireRiskCladdingSystemsId { get; set; }
}
