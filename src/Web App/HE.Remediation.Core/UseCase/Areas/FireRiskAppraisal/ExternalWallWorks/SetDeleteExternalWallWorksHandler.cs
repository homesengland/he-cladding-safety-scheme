
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.Administration.DeleteExtraContact.SetDeleteExtraContact;
using MediatR;
using System.Transactions;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.ExternalWallWorks;

public class SetDeleteExternalWallWorksHandler: IRequestHandler<SetDeleteExternalWallWorksRequest>
{
    private readonly IApplicationDataProvider _adp;
    private readonly IDbConnectionWrapper _db;
    private readonly IFireRiskWorksRepository _fireRiskWorksRepository;

    public SetDeleteExternalWallWorksHandler(IApplicationDataProvider adp, 
                                             IDbConnectionWrapper db,
                                             IFireRiskWorksRepository fireRiskWorksRepository)
    {
        _adp = adp;
        _db = db;
        _fireRiskWorksRepository = fireRiskWorksRepository;
    }

    public async Task<Unit> Handle(SetDeleteExternalWallWorksRequest request, CancellationToken cancellationToken)
    {
        var userId = _adp.GetUserId();

        if ((userId is null) || (request.Id is null))
        {
            throw new EntityNotFoundException(
                "Cannot delete external wall works because the user could be determined.");
        }

        await _fireRiskWorksRepository.DeleteFireRiskWallWorks(request.Id.Value);
        return Unit.Value;
    }
}
