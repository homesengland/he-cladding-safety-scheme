using Mediator;
namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageSubmit.Submit.Get;

public class GetSubmitRequest : IRequest<GetSubmitResponse>
{
    private GetSubmitRequest()
    {
    }

    public static readonly GetSubmitRequest Request = new();
}
