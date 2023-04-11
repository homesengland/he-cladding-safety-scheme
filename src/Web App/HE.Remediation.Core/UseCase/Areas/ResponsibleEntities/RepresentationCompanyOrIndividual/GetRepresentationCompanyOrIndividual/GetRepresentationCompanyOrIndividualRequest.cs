using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.RepresentationCompanyOrIndividual.GetRepresentationCompanyOrIndividual;

public class GetRepresentationCompanyOrIndividualRequest : IRequest<GetRepresentationCompanyOrIndividualResponse>
{
    private GetRepresentationCompanyOrIndividualRequest()
    {
    }

    public static readonly GetRepresentationCompanyOrIndividualRequest Request = new();
}