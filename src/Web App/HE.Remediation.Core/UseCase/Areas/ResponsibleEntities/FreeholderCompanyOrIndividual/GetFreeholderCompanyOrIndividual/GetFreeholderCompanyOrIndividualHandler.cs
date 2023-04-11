using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.FreeholderCompanyOrIndividual.GetFreeholderCompanyOrIndividual
{
    public class GetFreeholderCompanyOrIndividualHandler : IRequestHandler<GetFreeholderCompanyOrIndividualRequest, GetFreeholderCompanyOrIndividualResponse>
    {
        private readonly IDbConnectionWrapper _connection;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public GetFreeholderCompanyOrIndividualHandler(IDbConnectionWrapper connection, IApplicationDataProvider applicationDataProvider)
        {
            _connection = connection;
            _applicationDataProvider = applicationDataProvider;
        }

        public async Task<GetFreeholderCompanyOrIndividualResponse> Handle(GetFreeholderCompanyOrIndividualRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var response = await GetFreeholderCompanyOrIndividual(applicationId);

            return response;
        }

        private async Task<GetFreeholderCompanyOrIndividualResponse> GetFreeholderCompanyOrIndividual(Guid applicationId)
        {
            var reponsibleEntityTypeId = await _connection.QuerySingleOrDefaultAsync<int>("GetFreeholderCompanyOrIndividual", new { applicationId });

            return new GetFreeholderCompanyOrIndividualResponse
            {
                ReponsibleEntityType = (EResponsibleEntityType?)reponsibleEntityTypeId
            };
        }
    }
}