using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting;

public class GetBuildingControlDecisionHandler : IRequestHandler<GetBuildingControlDecisionRequest, GetBuildingControlDecisionResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IProgressReportingRepository _progressReportingRepository;

    public GetBuildingControlDecisionHandler(
        IApplicationDataProvider applicationDataProvider, 
        IApplicationRepository applicationRepository, 
        IBuildingDetailsRepository buildingDetailsRepository, 
        IProgressReportingRepository progressReportingRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _applicationRepository = applicationRepository;
        _buildingDetailsRepository = buildingDetailsRepository;
        _progressReportingRepository = progressReportingRepository;
    }

    public async Task<GetBuildingControlDecisionResponse> Handle(GetBuildingControlDecisionRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();
        var progressReportId = _applicationDataProvider.GetProgressReportId();

        var reference = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var decision = await _progressReportingRepository.GetBuildingControlDecision(
            new GetBuildingControlDecisionParameters
            {
                ApplicationId = applicationId,
                ProgressReportId = progressReportId
            });

        var version = await _progressReportingRepository.GetProgressReportVersion();

        return new GetBuildingControlDecisionResponse
        {
            ApplicationReferenceNumber = reference,
            BuildingName = buildingName,
            BuildingControlRequired = decision.BuildingControlRequired,
            Decision = decision.BuildingControlDecision,
            DecisionDateMonth = decision.BuildingControlDecisionDate?.Month,
            DecisionDateYear = decision.BuildingControlDecisionDate?.Year,
            DecisionInformation = decision.BuildingControlDecisionInformation,
            Version = version
        };
    }
}

public class GetBuildingControlDecisionRequest : IRequest<GetBuildingControlDecisionResponse>
{
    private GetBuildingControlDecisionRequest()
    {
    }

    public static readonly GetBuildingControlDecisionRequest Request = new();
}

public class GetBuildingControlDecisionResponse
{
    public bool? BuildingControlRequired { get; set; }

    public int? DecisionDateMonth { get; set; }
    public int? DecisionDateYear { get; set; }
    public bool? Decision { get; set; }
    public string DecisionInformation { get; set; }

    public string BuildingName { get; set; }
    public string ApplicationReferenceNumber { get; set; }
    public int Version { get; set; }
}