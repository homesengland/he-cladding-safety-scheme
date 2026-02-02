using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting;

public class GetHasGrantCertifyingOfficerHandler : IRequestHandler<GetHasGrantCertifyingOfficerRequest, GetHasGrantCertifyingOfficerResponse>
{
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IProgressReportingRepository _progressReportingRepository;
    private readonly IApplicationDataProvider _applicationDataProvider;

    public GetHasGrantCertifyingOfficerHandler(
        IApplicationRepository applicationRepository, 
        IBuildingDetailsRepository buildingDetailsRepository, 
        IProgressReportingRepository progressReportingRepository, 
        IApplicationDataProvider applicationDataProvider)
    {
        _applicationRepository = applicationRepository;
        _buildingDetailsRepository = buildingDetailsRepository;
        _progressReportingRepository = progressReportingRepository;
        _applicationDataProvider = applicationDataProvider;
    }

    public async ValueTask<GetHasGrantCertifyingOfficerResponse> Handle(GetHasGrantCertifyingOfficerRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();
        var reference = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var hasGco = await _progressReportingRepository.GetHasGrantCertifyingOfficer();
        var version = await _progressReportingRepository.GetProgressReportVersion();
        var gcoComplete = await _progressReportingRepository.IsGrantCertifyingOfficerComplete();

        var hasVisitedCheckYourAnswers = await _progressReportingRepository.GetHasVisitedCheckYourAnswers(
            new GetHasVisitedCheckYourAnswersParameters
            {
                ApplicationId = applicationId,
                ProgressReportId = _applicationDataProvider.GetProgressReportId()
            });

        return new GetHasGrantCertifyingOfficerResponse
        {
            ApplicationReferenceNumber = reference,
            BuildingName = buildingName,
            DoYouHaveAGrantCertifyingOfficer = hasGco,
            Version = version,
            IsGcoComplete = gcoComplete,
            HasVisitedCheckYourAnswers = hasVisitedCheckYourAnswers
        };
    }
}

public class GetHasGrantCertifyingOfficerRequest : IRequest<GetHasGrantCertifyingOfficerResponse>
{
    private GetHasGrantCertifyingOfficerRequest()
    {
    }

    public static readonly GetHasGrantCertifyingOfficerRequest Request = new();
}

public class GetHasGrantCertifyingOfficerResponse
{
    public string BuildingName { get; set; }
    public string ApplicationReferenceNumber { get; set; }
    public string ReturnUrl { get; set; }

    public bool? DoYouHaveAGrantCertifyingOfficer { get; set; }
    public int Version { get; set; }
    public bool IsGcoComplete { get; set; }
    public bool HasVisitedCheckYourAnswers { get; set; }
}