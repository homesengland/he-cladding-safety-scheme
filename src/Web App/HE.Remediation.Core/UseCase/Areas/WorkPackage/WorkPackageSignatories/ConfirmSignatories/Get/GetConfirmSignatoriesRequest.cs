using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageSignatories.ConfirmSignatories.Get;

public class GetConfirmSignatoriesRequest : IRequest<GetConfirmSignatoriesResponse>
{
    private GetConfirmSignatoriesRequest()
    {
    }

    public static GetConfirmSignatoriesRequest Request => new();
}
