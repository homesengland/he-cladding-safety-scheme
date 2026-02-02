using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting;

public class SetGcoDetailsHandler : IRequestHandler<SetGcoDetailsRequest>
{
    private readonly IProgressReportingRepository _progressReportingRepository;

    public SetGcoDetailsHandler(IProgressReportingRepository progressReportingRepository)
    {
        _progressReportingRepository = progressReportingRepository;
    }

    public async ValueTask<Unit> Handle(SetGcoDetailsRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _progressReportingRepository.UpdateGrantCertifiyingOfficerResponse(request.CertifyingOfficerResponse!.Value);

        if (request.CertifyingOfficerResponse == ECertifyingOfficerResponse.Yes)
        {
            await _progressReportingRepository.UpdateGrantCertifyingOfficerDetails();
        }

        scope.Complete();

        return Unit.Value;
    }
}

public class SetGcoDetailsRequest : IRequest
{
    public ECertifyingOfficerResponse? CertifyingOfficerResponse { get; set; }
}