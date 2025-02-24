using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.GrantFundingSignatories.DeleteGrantFundingSignatory
{
    public class DeleteGrantFundingSignatoryHandler : IRequestHandler<DeleteGrantFundingSignatoryRequest>
    {
        private readonly IDbConnectionWrapper _connection;

        public DeleteGrantFundingSignatoryHandler(IDbConnectionWrapper connection)
        {
            _connection = connection;
        }

        public async Task<Unit> Handle(DeleteGrantFundingSignatoryRequest request, CancellationToken cancellationToken)
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
