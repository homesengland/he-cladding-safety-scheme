using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.EvidenceOfThirdPartyContribution.GetYesNoDeclaration;

public class GetYesNoDeclarationHandler : IRequestHandler<GetYesNoDeclarationRequest, GetYesNoDeclarationResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IClosingReportRepository _closingReportRepository;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;

    public GetYesNoDeclarationHandler(IApplicationDataProvider applicationDataProvider, 
        IClosingReportRepository closingReportRepository,
        IApplicationRepository applicationRepository,
        IBuildingDetailsRepository buildingDetailsRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _closingReportRepository = closingReportRepository;
        _applicationRepository = applicationRepository;
        _buildingDetailsRepository = buildingDetailsRepository;
    }

    public async Task<GetYesNoDeclarationResponse> Handle(GetYesNoDeclarationRequest request,
        CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var closingReport = await _closingReportRepository.GetClosingReportDetails(applicationId);

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        if (closingReport.HasThirdPartyContributions.HasValue)
        {
            return new GetYesNoDeclarationResponse() { Declaration = closingReport.HasThirdPartyContributions.Value ? ENoYes.Yes : ENoYes.No, IsSubmitted = closingReport.IsSubmitted, ApplicationReferenceNumber = applicationReferenceNumber, BuildingName = buildingName };
        }

        return new GetYesNoDeclarationResponse() { Declaration = null, ApplicationReferenceNumber = applicationReferenceNumber, BuildingName = buildingName };
    }
}