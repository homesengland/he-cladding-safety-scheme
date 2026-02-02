using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.Representative.GetRepresentativeType;

public class GetRepresentativeTypeRequest : IRequest<GetRepresentativeTypeResponse>
{
    private GetRepresentativeTypeRequest()
    {
    }

    public static readonly GetRepresentativeTypeRequest Request = new();
}