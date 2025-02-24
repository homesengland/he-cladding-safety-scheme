using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.Start;

public class CheckSubmittedStatusHandler : IRequestHandler<CheckSubmittedStatusRequest, bool>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IProgressReportingRepository _progressReportingRepository;

    public CheckSubmittedStatusHandler(
        IApplicationDataProvider applicationDataProvider, 
        IProgressReportingRepository progressReportingRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _progressReportingRepository = progressReportingRepository;
    }

    public async Task<bool> Handle(CheckSubmittedStatusRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        var isSubmitted = await _progressReportingRepository.IsProgressReportSubmitted(applicationId, request.ProgressReportId);
        return isSubmitted;
    }
}