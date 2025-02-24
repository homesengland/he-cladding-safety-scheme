using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters.WorkPackage.CostsScheduling;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.Costs.InstallationOfCladding;

public class SetInstallationOfCladdingHandler : IRequestHandler<SetInstallationOfCladdingRequest>
{
    private readonly IWorkPackageRepository _workPackageRepository;

    public SetInstallationOfCladdingHandler(IWorkPackageRepository workPackageRepository)
    {
        _workPackageRepository = workPackageRepository;
    }

    public async Task<Unit> Handle(SetInstallationOfCladdingRequest request, CancellationToken cancellationToken)
    {
        await _workPackageRepository.UpdateInstallationOfCladdingCosts(new UpdateInstallationOfCladdingCostsParameters
        {
            NewCladdingAmount = request.NewCladdingAmount,
            NewCladdingDescription = request.NewCladdingDescription,
            ExternalWorksAmount = request.ExternalWorksAmount,
            ExternalWorksDescription = request.ExternalWorksDescription,
            InternalWorksAmount = request.InternalWorksAmount,
            InternalWorksDescription = request.InternalWorksDescription
        });

        return Unit.Value;
    }
}