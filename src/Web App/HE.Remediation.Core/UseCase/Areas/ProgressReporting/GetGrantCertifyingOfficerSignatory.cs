using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting;

public class GetGrantCertifyingOfficerSignatoryHandler : IRequestHandler<GetGrantCertifyingOfficerSignatoryRequest, GetGrantCertifyingOfficerSignatoryResponse>
{
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IProgressReportingRepository _progressReportingRepository;
    private readonly IApplicationDataProvider _applicationDataProvider;

    public GetGrantCertifyingOfficerSignatoryHandler(IApplicationRepository applicationRepository, IBuildingDetailsRepository buildingDetailsRepository, IProgressReportingRepository progressReportingRepository, IApplicationDataProvider applicationDataProvider)
    {
        _applicationRepository = applicationRepository;
        _buildingDetailsRepository = buildingDetailsRepository;
        _progressReportingRepository = progressReportingRepository;
        _applicationDataProvider = applicationDataProvider;
    }

    public async Task<GetGrantCertifyingOfficerSignatoryResponse> Handle(GetGrantCertifyingOfficerSignatoryRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();
        var reference = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var result = await _progressReportingRepository.GetGrantCertifyingOfficerSignatory();
        var version = await _progressReportingRepository.GetProgressReportVersion();
        var isGcoComplete = await _progressReportingRepository.IsGrantCertifyingOfficerComplete();

        return new GetGrantCertifyingOfficerSignatoryResponse
        {
            ApplicationReferenceNumber = reference,
            BuildingName = buildingName,
            Signatory = result.AuthorisedSignatory1,
            EmailAddress = result.AuthorisedSignatory1EmailAddress,
            DateAppointedDay = result.DateAppointed?.Day,
            DateAppointedMonth = result.DateAppointed?.Month,
            DateAppointedYear = result.DateAppointed?.Year,
            Version = version,
            IsGcoComplete = isGcoComplete
        };
    }
}

public class GetGrantCertifyingOfficerSignatoryRequest : IRequest<GetGrantCertifyingOfficerSignatoryResponse>
{
    private GetGrantCertifyingOfficerSignatoryRequest()
    {
    }

    public static readonly GetGrantCertifyingOfficerSignatoryRequest Request = new();
}

public class GetGrantCertifyingOfficerSignatoryResponse
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }

    public string Signatory { get; set; }
    public string EmailAddress { get; set; }
    public int? DateAppointedDay { get; set; }
    public int? DateAppointedMonth { get; set; }
    public int? DateAppointedYear { get; set; }

    public int Version { get; set; }
    public bool IsGcoComplete { get; set; }
}