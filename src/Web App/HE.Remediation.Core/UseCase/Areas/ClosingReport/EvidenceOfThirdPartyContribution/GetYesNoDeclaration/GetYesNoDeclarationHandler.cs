using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.EvidenceOfThirdPartyContribution.GetYesNoDeclaration;

public class GetYesNoDeclarationHandler : IRequestHandler<GetYesNoDeclarationRequest, GetYesNoDeclarationResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IClosingReportRepository _closingReportRepository;

    public GetYesNoDeclarationHandler(IApplicationDataProvider applicationDataProvider, IClosingReportRepository closingReportRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _closingReportRepository = closingReportRepository;
    }

    public async Task<GetYesNoDeclarationResponse> Handle(GetYesNoDeclarationRequest request,
        CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var closingReport = await _closingReportRepository.GetClosingReportDetails(applicationId);

        if (closingReport.HasThirdPartyContributions.HasValue)
        {
            return new GetYesNoDeclarationResponse() { Declaration = closingReport.HasThirdPartyContributions.Value ? ENoYes.Yes : ENoYes.No, IsSubmitted = closingReport.IsSubmitted };
        }

        return new GetYesNoDeclarationResponse() { Declaration = null };
    }
}