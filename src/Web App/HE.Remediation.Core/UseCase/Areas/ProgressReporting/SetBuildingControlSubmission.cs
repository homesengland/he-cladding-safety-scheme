using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting;

public class SetBuildingControlSubmissionHandler : IRequestHandler<SetBuildingControlSubmissionRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IProgressReportingRepository _progressReportingRepository;

    public SetBuildingControlSubmissionHandler(
        IApplicationDataProvider applicationDataProvider, 
        IProgressReportingRepository progressReportingRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _progressReportingRepository = progressReportingRepository;
    }

    public async Task<Unit> Handle(SetBuildingControlSubmissionRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        await _progressReportingRepository.UpdateBuildingControlSubmission(new UpdateBuildingControlSubmissionParameters
        {
            ApplicationId = _applicationDataProvider.GetApplicationId(),
            ProgressReportId = _applicationDataProvider.GetProgressReportId(),
            SubmissionDate = request.SubmissionDate,
            BuildingControlApplicationReference = request.BuildingControlApplicationReference,
            SubmissionInformation = request.SubmissionInformation
        });

        return Unit.Value;
    }
}

public class SetBuildingControlSubmissionRequest : IRequest
{
    public DateTime? SubmissionDate { get; set; }
    public string BuildingControlApplicationReference { get; set; }
    public string SubmissionInformation { get; set; }
}