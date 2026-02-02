using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters.WorkPackage.GrantCertifyingOfficer;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageGrantCertifyingOfficer.AddressDetails.SetManual;

public class SetAddressManualDetailsHandler : IRequestHandler<SetAddressManualDetailsRequest>
{
    private readonly IWorkPackageRepository _workPackageRepository;

    public SetAddressManualDetailsHandler(IWorkPackageRepository workPackageRepository)
    {
        _workPackageRepository = workPackageRepository;
    }

    public async ValueTask<Unit> Handle(SetAddressManualDetailsRequest request, CancellationToken cancellationToken)
    {
        await _workPackageRepository.UpdateWorkPackageGrantCertifyingOfficerAddress(
            new UpdateWorkPackageGrantCertifyingOfficerAddressParameters
            {
                NameNumber = request.NameNumber,
                AddressLine1 = request.AddressLine1,
                AddressLine2 = request.AddressLine2,
                City = request.City,
                County = request.County,
                Postcode = request.Postcode
            });

        return Unit.Value;
    }
}
