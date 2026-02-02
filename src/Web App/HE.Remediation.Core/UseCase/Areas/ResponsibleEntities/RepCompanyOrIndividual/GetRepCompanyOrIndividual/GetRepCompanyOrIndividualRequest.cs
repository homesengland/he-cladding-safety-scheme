using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.RepCompanyOrIndividual.GetRepCompanyOrIndividual;

public class GetRepCompanyOrIndividualRequest : IRequest<GetRepCompanyOrIndividualResponse>
{
    private GetRepCompanyOrIndividualRequest()
    {
    }

    public static readonly GetRepCompanyOrIndividualRequest Request = new();
}