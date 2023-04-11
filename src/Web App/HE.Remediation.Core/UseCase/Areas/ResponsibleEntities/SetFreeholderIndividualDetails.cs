using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities
{
    public class SetFreeholderIndividualDetailsHandler : IRequestHandler<SetFreeholderIndividualDetailsRequest>
    {
        private readonly IDbConnectionWrapper _connection;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public SetFreeholderIndividualDetailsHandler(IDbConnectionWrapper connection, IApplicationDataProvider applicationDataProvider)
        {
            _connection = connection;
            _applicationDataProvider = applicationDataProvider;
        }

        public async Task<Unit> Handle(SetFreeholderIndividualDetailsRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            await UpdateFreeholderIndividualDetails(applicationId, request);

            return Unit.Value;
        }

        private async Task UpdateFreeholderIndividualDetails(Guid applicationId, SetFreeholderIndividualDetailsRequest request)
        {
            await _connection.ExecuteAsync("UpdateFreeholderIndividualDetails",
                new
                {
                    applicationId,
                    request.FirstName,
                    request.LastName,
                    request.EmailAddress,
                    request.ContactNumber
                });
        }
    }

    public class SetFreeholderIndividualDetailsRequest : IRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string ContactNumber { get; set; }
    }
}
