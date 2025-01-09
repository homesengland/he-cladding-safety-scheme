using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.GetDeclaration;

public class GetDeclarationHandler : IRequestHandler<GetDeclarationRequest, GetDeclarationResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IClosingReportRepository _closingReportRepository;

    public GetDeclarationHandler(IApplicationDataProvider applicationDataProvider,
                                 IApplicationRepository applicationRepository,
                                 IBuildingDetailsRepository buildingDetailsRepository,
                                 IClosingReportRepository closingReportRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _applicationRepository = applicationRepository;
        _buildingDetailsRepository = buildingDetailsRepository;
        _closingReportRepository = closingReportRepository;
    }

    public async Task<GetDeclarationResponse> Handle(GetDeclarationRequest request,
        CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var isSubmitted = await _closingReportRepository.IsClosingReportSubmitted(applicationId);
        var closingReportDetails = await _closingReportRepository.GetClosingReportDetails(applicationId);

        var applicationCreationDate = await _applicationRepository.GetApplicationCreationDate(applicationId);

        return new GetDeclarationResponse
        {
            ApplicationCreationDate = applicationCreationDate,
            DateOfCompletion = closingReportDetails?.ProjectCompletionDate,
            LifeSafetyRiskAssessment = closingReportDetails?.LifeSafetyRiskAssessment,
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber,
            IsSubmitted = isSubmitted
        };
    }
}
