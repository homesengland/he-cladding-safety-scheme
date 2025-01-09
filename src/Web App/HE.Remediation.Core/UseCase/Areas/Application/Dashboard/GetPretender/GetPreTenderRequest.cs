using MediatR;
namespace HE.Remediation.Core.UseCase.Areas.Application.Dashboard.GetPretender;

public class GetPreTenderRequest: IRequest<GetPreTenderResponse>
{
    private GetPreTenderRequest()
    {
    }

    public static readonly GetPreTenderRequest Request = new();
}
