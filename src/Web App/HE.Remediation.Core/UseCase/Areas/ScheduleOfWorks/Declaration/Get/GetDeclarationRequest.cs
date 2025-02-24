using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.Declaration.Get;

public class GetDeclarationRequest : IRequest<GetDeclarationResponse>
{
    private GetDeclarationRequest()
    {
    }

    public static GetDeclarationRequest Request => new();
}
