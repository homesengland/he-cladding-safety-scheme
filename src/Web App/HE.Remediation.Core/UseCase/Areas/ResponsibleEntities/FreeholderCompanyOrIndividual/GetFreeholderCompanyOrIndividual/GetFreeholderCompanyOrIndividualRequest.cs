using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.FreeholderCompanyOrIndividual.GetFreeholderCompanyOrIndividual
{
    public class GetFreeholderCompanyOrIndividualRequest : IRequest<GetFreeholderCompanyOrIndividualResponse>
    {
        private GetFreeholderCompanyOrIndividualRequest() { }

        public static readonly GetFreeholderCompanyOrIndividualRequest Request = new();
    }
}
