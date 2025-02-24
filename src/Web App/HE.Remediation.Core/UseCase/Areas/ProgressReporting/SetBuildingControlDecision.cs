using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting;

public class SetBuildingControlDecisionHandler : IRequestHandler<SetBuildingControlDecisionRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IProgressReportingRepository _progressReportingRepository;

    public SetBuildingControlDecisionHandler(
        IApplicationDataProvider applicationDataProvider, 
        IProgressReportingRepository progressReportingRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _progressReportingRepository = progressReportingRepository;
    }

    public async Task<Unit> Handle(SetBuildingControlDecisionRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        await _progressReportingRepository.UpdateBuildingControlDecision(new UpdateBuildingControlDecisionParameters
        {
            ApplicationId = _applicationDataProvider.GetApplicationId(),
            ProgressReportId = _applicationDataProvider.GetProgressReportId(),
            DecisionDate = request.DecisionDate,
            BuildingControlDecision = request.Decision,
            DecisionInformation = request.DecisionInformation
        });

        return Unit.Value;
    }
}

public class SetBuildingControlDecisionRequest : IRequest
{
    public DateTime? DecisionDate { get; set; }
    public bool? Decision { get; set; }
    public string DecisionInformation { get; set; }
}