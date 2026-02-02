using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.UserService.Model;
using HE.Remediation.Core.UseCase.Areas.Administration.SecondaryContactDetails.SetSecondaryContactDetails;
using Mediator;
using System.Transactions;

namespace HE.Remediation.Core.UseCase.Areas.Administration.DeleteExtraContact.SetDeleteExtraContact;

public class SetDeleteExtraContactHandler: IRequestHandler<SetDeleteExtraContactRequest>
{
    private readonly IApplicationDataProvider _adp;
    private readonly IDbConnectionWrapper _db;

    public SetDeleteExtraContactHandler(IApplicationDataProvider adp, IDbConnectionWrapper db)
    {
        _adp = adp;
        _db = db;
    }

    public async ValueTask<Unit> Handle(SetDeleteExtraContactRequest request, CancellationToken cancellationToken)
    {
        var userId = _adp.GetUserId();

        if ((userId is null) || (request.Id is null))
        {
            throw new EntityNotFoundException(
                "Cannot delete extra contact because the contact could be determined.");
        }

        using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            await _db.ExecuteAsync("DeleteAdditionalContact", new { @UserId = userId, @Id=request.Id });            
            scope.Complete();
        }

     return Unit.Value;
    }
}

