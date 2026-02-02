using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.InternalWallWorks;

public class SetDeleteInternalWallWorksHandler: IRequestHandler<SetDeleteInternalWallWorksRequest>
{
    private readonly IApplicationDataProvider _adp;
    private readonly IDbConnectionWrapper _db;
    private readonly IFireRiskWorksRepository _fireRiskWorksRepository;

    public SetDeleteInternalWallWorksHandler(IApplicationDataProvider adp, 
                                             IDbConnectionWrapper db,
                                             IFireRiskWorksRepository fireRiskWorksRepository)
    {
        _adp = adp;
        _db = db;
        _fireRiskWorksRepository = fireRiskWorksRepository;
    }

    public async ValueTask<Unit> Handle(SetDeleteInternalWallWorksRequest request, CancellationToken cancellationToken)
    {
        var userId = _adp.GetUserId();

        if (userId is null)
        {
            throw new EntityNotFoundException(
                "Cannot delete external wall works because the user could be determined.");
        }

        await _fireRiskWorksRepository.DeleteFireRiskWallWorks(request.Id);
        return Unit.Value;
    }
}
