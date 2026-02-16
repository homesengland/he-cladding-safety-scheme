using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetAddRole;

public class GetAddRoleRequest : IRequest<GetAddRoleResponse>
{
    private GetAddRoleRequest()
    {
    }

    public static readonly GetAddRoleRequest Request = new();
}
