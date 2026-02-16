using HE.Remediation.Core.Data.Repositories;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting;

public class GetIsGcoCompleteHandler : IRequestHandler<GetIsGcoCompleteRequest, bool>
{
    private readonly IProgressReportingRepository _progressReportingRepository;

    public GetIsGcoCompleteHandler(IProgressReportingRepository progressReportingRepository)
    {
        _progressReportingRepository = progressReportingRepository;
    }

    public async ValueTask<bool> Handle(GetIsGcoCompleteRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var isGcoComplete = await _progressReportingRepository.IsGrantCertifyingOfficerComplete();
        return isGcoComplete;
    }
}

public class GetIsGcoCompleteRequest : IRequest<bool>
{
    private GetIsGcoCompleteRequest()
    {
    }

    public static readonly GetIsGcoCompleteRequest Request = new();
}