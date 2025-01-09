using HE.Remediation.Core.Data.Repositories;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageKeyDates.IsCladdingBeingRemoved.Get;

public class GetKeyDatesHandler : IRequestHandler<GetIsCladdingBeingRemovedRequest, bool>
{
    private readonly IWorkPackageRepository _workPackageRepository;

    public GetKeyDatesHandler(IWorkPackageRepository workPackageRepository)
    {
        _workPackageRepository = workPackageRepository;
    }

    public async Task<bool> Handle(GetIsCladdingBeingRemovedRequest request, CancellationToken cancellationToken)
    {
        var isCladdingBeingRemoved = await _workPackageRepository.IsCladdingBeingRemoved();

        return isCladdingBeingRemoved;
    }
}
