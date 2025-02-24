using HE.Remediation.Core.Data.Repositories;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.NoQuotes.Set;

public class SetNoQuotesHandler : IRequestHandler<SetNoQuotesRequest>
{
    private readonly IWorkPackageRepository _workPackageRepository;

    public SetNoQuotesHandler(IWorkPackageRepository workPackageRepository)
    {
        _workPackageRepository = workPackageRepository;
    }

    public async Task<Unit> Handle(SetNoQuotesRequest request, CancellationToken cancellationToken)
    {
        await _workPackageRepository.UpdateCostsScheduleNoQuotesReason(request.Reason);

        return Unit.Value;
    }
}
