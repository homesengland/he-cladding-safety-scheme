using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.RepresentativeBasedInUk.GetRepresentativeBasedInUk;

public class GetRepresentativeBasedInUkRequest : IRequest<GetRepresentativeBasedInUkResponse>
{
    private GetRepresentativeBasedInUkRequest()
    {
    }

    public static readonly GetRepresentativeBasedInUkRequest Request = new();
}