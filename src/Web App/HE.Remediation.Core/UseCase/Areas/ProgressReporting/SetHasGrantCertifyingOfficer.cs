using HE.Remediation.Core.Data.Repositories;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting;

public class SetHasGrantCertifyingOfficerHandler : IRequestHandler<SetHasGrantCertifyingOfficerRequest>
{
    private readonly IProgressReportingRepository _progressReportingRepository;

    public SetHasGrantCertifyingOfficerHandler(IProgressReportingRepository progressReportingRepository)
    {
        _progressReportingRepository = progressReportingRepository;
    }

    public async ValueTask<Unit> Handle(SetHasGrantCertifyingOfficerRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        await _progressReportingRepository.UpdateHasGrantCertifyingOfficer(request.DoYouHaveAGrantCertifyingOfficer!.Value);

        return Unit.Value;
    }
}

public class SetHasGrantCertifyingOfficerRequest : IRequest
{
    public bool? DoYouHaveAGrantCertifyingOfficer { get; set; }
}