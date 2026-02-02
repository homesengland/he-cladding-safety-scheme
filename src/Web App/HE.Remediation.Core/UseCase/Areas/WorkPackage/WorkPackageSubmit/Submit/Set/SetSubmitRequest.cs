using Mediator;
namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageSubmit.Submit.Set;

public class SetSubmitRequest : IRequest<Unit>
{
    private SetSubmitRequest()
    {
    }

    public static readonly SetSubmitRequest Request = new();
}
