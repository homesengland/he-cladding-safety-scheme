using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.Submitted.GetSubmitted;

public class GetSubmittedHandler : IRequestHandler<GetSubmittedRequest, GetSubmittedResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IProgressReportingRepository _progressReportingRepository;

    public GetSubmittedHandler(IApplicationDataProvider applicationDataProvider,
                               IApplicationRepository applicationRepository,
                               IProgressReportingRepository progressReportingRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _applicationRepository = applicationRepository;
        _progressReportingRepository = progressReportingRepository;
    }

    public async ValueTask<GetSubmittedResponse> Handle(GetSubmittedRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var version = await _progressReportingRepository.GetProgressReportVersion();

        return new GetSubmittedResponse
        {
            ApplicationReferenceNumber = applicationReferenceNumber,
            Version = version
        };
    }
}
