using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.EvidenceOfThirdPartyContribution.GetYesNoDeclaration;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.EvidenceOfThirdPartyContribution.ReasonForNoContributions.Set;

public class SetReasonForNoContributionHandler : IRequestHandler<SetReasonForNoContributionsRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IEvidenceOfThirdPartyContributionRepository _evidenceOfThirdPartyContributionRepository;
    private readonly IClosingReportRepository _closingReportRepository;
    private readonly ILogger<SetYesNoDeclarationHandler> _logger;

    public SetReasonForNoContributionHandler(
        IApplicationDataProvider applicationDataProvider,
        IEvidenceOfThirdPartyContributionRepository evidenceOfThirdPartyContributionRepository,
        IClosingReportRepository closingReportRepository,
        ILogger<SetYesNoDeclarationHandler> logger)
    {
        _applicationDataProvider = applicationDataProvider;
        _evidenceOfThirdPartyContributionRepository = evidenceOfThirdPartyContributionRepository;
        _closingReportRepository = closingReportRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(SetReasonForNoContributionsRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        await _closingReportRepository.UpdateClosingReportReasonForNoContributions(applicationId, request.ReasonNoThirdPartyContributions);

        
        return Unit.Value;
    }
}
