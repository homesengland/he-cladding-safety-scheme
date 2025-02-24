using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting;

public class GetIntentToProceedHandler : IRequestHandler<GetIntentToProceedRequest, GetIntentToProceedResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IProgressReportingRepository _progressReportingRepository;

    public GetIntentToProceedHandler(
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

    public async Task<GetIntentToProceedResponse> Handle(GetIntentToProceedRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        var applicationId = _applicationDataProvider.GetApplicationId();
        var progressReportId = _applicationDataProvider.GetProgressReportId();

        var applicationReference = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);
        var version = await _progressReportingRepository.GetProgressReportVersion();

        var hasGco = await _progressReportingRepository.GetHasGrantCertifyingOfficer();
        var appointedMembers = await _progressReportingRepository.GetProgressReportOtherMembersAppointed();


        var intent = await _progressReportingRepository.GetIntentToProceedType(new GetIntentToProceedTypeParameters
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId
        });

        return new GetIntentToProceedResponse
        {
            IntentToProceedType = intent,
            ApplicationReferenceNumber = applicationReference,
            BuildingName = buildingName,
            Version = version,
            HasGco = hasGco ?? false,
            OtherMembersAppointed = appointedMembers
        };
    }
}

public class GetIntentToProceedRequest : IRequest<GetIntentToProceedResponse>
{
    private GetIntentToProceedRequest()
    {
    }

    public static readonly GetIntentToProceedRequest Request = new();
}

public class GetIntentToProceedResponse
{
    public EIntentToProceedType? IntentToProceedType { get; set; }
    public int Version { get; set; }
    public bool? OtherMembersAppointed { get; set; }
    public bool HasGco { get; set; }


    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
}