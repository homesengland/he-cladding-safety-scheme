using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.EvidenceOfThirdPartyContribution.GetYesNoDeclaration;

using MediatR;


namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.EvidenceOfThirdPartyContribution.ReasonForNoContributions.Get;

public class GetReasonForNoContributionsHandler : IRequestHandler<GetReasonForNoContributionsRequest, GetReasonForNoContributionsResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IClosingReportRepository _closingReportRepository;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;

    public GetReasonForNoContributionsHandler(IApplicationDataProvider applicationDataProvider,
        IClosingReportRepository closingReportRepository,
        IApplicationRepository applicationRepository,
        IBuildingDetailsRepository buildingDetailsRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _closingReportRepository = closingReportRepository;
        _applicationRepository = applicationRepository;
        _buildingDetailsRepository = buildingDetailsRepository;
    }

    public async Task<GetReasonForNoContributionsResponse> Handle(GetReasonForNoContributionsRequest request,
        CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var closingReport = await _closingReportRepository.GetClosingReportDetails(applicationId);

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

       
        return new GetReasonForNoContributionsResponse()
        {
            ReasonNoThirdPartyContributions = closingReport.NoThirdPartyContributionsReason,
            IsSubmitted = closingReport.IsSubmitted,
            ApplicationReferenceNumber = applicationReferenceNumber,
            BuildingName = buildingName
        };
    }
}