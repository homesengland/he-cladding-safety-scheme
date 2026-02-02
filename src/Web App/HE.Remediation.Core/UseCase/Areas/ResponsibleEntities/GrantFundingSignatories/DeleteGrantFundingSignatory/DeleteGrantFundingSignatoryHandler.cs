using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.GrantFundingSignatories.DeleteGrantFundingSignatory
{
    public class DeleteGrantFundingSignatoryHandler : IRequestHandler<DeleteGrantFundingSignatoryRequest>
    {
        private readonly IDbConnectionWrapper _connection;

        public DeleteGrantFundingSignatoryHandler(IDbConnectionWrapper connection)
        {
            _connection = connection;
        }

        public async ValueTask<Unit> Handle(DeleteGrantFundingSignatoryRequest request, CancellationToken cancellationToken)
        {
            await _connection.ExecuteAsync("DeleteResponsibleEntitiesGrantFundingSignatory",
                    new
                    {
                        request.GrantFundingSignatoryId
                    });
            return Unit.Value;
        }
    }
}
