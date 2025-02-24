using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.CladdingSystemCheckYourAnswers.Set;

public class SetCladdingSystemCheckYourAnswersRequest : IRequest<Unit>
{
    public Guid FireRiskCladdingSystemsId { get; set; }
}
