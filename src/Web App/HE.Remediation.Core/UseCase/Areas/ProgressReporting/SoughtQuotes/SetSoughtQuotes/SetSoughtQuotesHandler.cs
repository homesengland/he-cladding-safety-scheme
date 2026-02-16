
using HE.Remediation.Core.Data.Repositories;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.SoughtQuotes.SetSoughtQuotes;

public class SetSoughtQuotesHandler : IRequestHandler<SetSoughtQuotesRequest>
{
    private readonly IProgressReportingRepository _progressReportingRepository;

    public SetSoughtQuotesHandler(IProgressReportingRepository progressReportingRepository)
    {
        _progressReportingRepository = progressReportingRepository;
    }

    public async ValueTask<Unit> Handle(SetSoughtQuotesRequest request, CancellationToken cancellationToken)
    {
        await _progressReportingRepository.UpdateQuotesSought(request.QuotesSought);
        return Unit.Value;
    }
}
