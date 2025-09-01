using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.EvidenceOfThirdPartyContribution.GetYesNoDeclaration;

public class SetYesNoDeclarationHandler : IRequestHandler<SetYesNoDeclarationRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IEvidenceOfThirdPartyContributionRepository _evidenceOfThirdPartyContributionRepository;
    private readonly IClosingReportRepository _closingReportRepository;
    private readonly ILogger<SetYesNoDeclarationHandler> _logger;

    public SetYesNoDeclarationHandler(
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

    public async Task<Unit> Handle(SetYesNoDeclarationRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        await _closingReportRepository.UpdateClosingReportHasThirdPartyContributions(applicationId, request.HasThirdPartyContributions);

        if (request.HasThirdPartyContributions)
        {
            try
            {
                await _evidenceOfThirdPartyContributionRepository.ImportClosingReportEvidenceDetail(applicationId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Could not import third party evidence to Closing Report");
            }
        }

        return Unit.Value;
    }
}

public class SetYesNoDeclarationRequest(bool hasThirdPartyContributions) : IRequest<Unit>
{
    public bool HasThirdPartyContributions { get; } = hasThirdPartyContributions;
}