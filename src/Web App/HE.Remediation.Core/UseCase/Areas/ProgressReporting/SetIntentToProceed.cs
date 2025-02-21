using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting;

public class SetIntentToProceedHandler : IRequestHandler<SetIntentToProceedRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IProgressReportingRepository _progressReportingRepository;

    public SetIntentToProceedHandler(IApplicationDataProvider applicationDataProvider, IProgressReportingRepository progressReportingRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _progressReportingRepository = progressReportingRepository;
    }

    public async Task<Unit> Handle(SetIntentToProceedRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();
        var progressReportId = _applicationDataProvider.GetProgressReportId();

        await _progressReportingRepository.UpdateIntentToProceedType(new UpdateIntentToProceedTypeParameters
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId,
            IntentToProceedTypeId = (int)request.IntentToProceedType!.Value
        });

        return Unit.Value;
    }
}

public class SetIntentToProceedRequest : IRequest
{
    public EIntentToProceedType? IntentToProceedType { get; set; }
}