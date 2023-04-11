using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities
{
    public class SetResponsibleEntityPrimaryContactDetailsHandler : IRequestHandler<SetResponsibleEntityPrimaryContactDetailsRequest>
    {
        private readonly IDbConnectionWrapper _connection;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public SetResponsibleEntityPrimaryContactDetailsHandler(IDbConnectionWrapper connection, IApplicationDataProvider applicationDataProvider)
        {
            _connection = connection;
            _applicationDataProvider = applicationDataProvider;
        }

        public async Task<Unit> Handle(SetResponsibleEntityPrimaryContactDetailsRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            await UpdateResponsibleEntityPrimaryContactDetails(applicationId, request);

            return Unit.Value;
        }

        private async Task UpdateResponsibleEntityPrimaryContactDetails(Guid applicationId, SetResponsibleEntityPrimaryContactDetailsRequest request)
        {
            await _connection.ExecuteAsync("UpdateResponsibleEntityPrimaryContactDetails",
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

    public class SetResponsibleEntityPrimaryContactDetailsRequest : IRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string ContactNumber { get; set; }
    }
}
