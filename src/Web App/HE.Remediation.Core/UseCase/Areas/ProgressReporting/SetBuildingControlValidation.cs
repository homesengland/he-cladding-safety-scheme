using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting;

public class SetBuildingControlValidationHandler : IRequestHandler<SetBuildingControlValidationRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IProgressReportingRepository _progressReportingRepository;

    public SetBuildingControlValidationHandler(
        IApplicationDataProvider applicationDataProvider, 
        IProgressReportingRepository progressReportingRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _progressReportingRepository = progressReportingRepository;
    }

    public async ValueTask<Unit> Handle(SetBuildingControlValidationRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        await _progressReportingRepository.UpdateBuildingControlValidation(new UpdateBuildingControlValidationParameters
        {
            ApplicationId = _applicationDataProvider.GetApplicationId(),
            ProgressReportId = _applicationDataProvider.GetProgressReportId(),
            ValidationDate = request.ValidationDate,
            ValidationInformation = request.ValidationInformation
        });

        return Unit.Value;
    }
}

public class SetBuildingControlValidationRequest : IRequest
{
    public DateTime? ValidationDate { get; set; }
    public string ValidationInformation { get; set; }
}