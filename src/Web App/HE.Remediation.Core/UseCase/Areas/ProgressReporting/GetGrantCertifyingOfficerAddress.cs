using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting;

public class GetGrantCertifyingOfficerAddressHandler : IRequestHandler<GetGrantCertifyingOfficerAddressRequest, GetGrantCertifyingOfficerAddressResponse>
{
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IProgressReportingRepository _progressReportingRepository;
    private readonly IApplicationDataProvider _applicationDataProvider;

    public GetGrantCertifyingOfficerAddressHandler(
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

    public async Task<GetGrantCertifyingOfficerAddressResponse> Handle(GetGrantCertifyingOfficerAddressRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();
        var reference = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var address = await _progressReportingRepository.GetGrantCertifyingOfficerAddress();
        var version = await _progressReportingRepository.GetProgressReportVersion();
        var isGcoComplete = await _progressReportingRepository.IsGrantCertifyingOfficerComplete();

        return new GetGrantCertifyingOfficerAddressResponse
        {
            BuildingName = buildingName,
            ApplicationReferenceNumber = reference,
            NameNumber = address?.NameNumber,
            AddressLine1 = address?.AddressLine1,
            AddressLine2 = address?.AddressLine2,
            City = address?.City,
            County = address?.County,
            PostCode = address?.Postcode,
            ReturnUrl = (address?.CertifyingOfficerResponseId).HasValue && address?.CertifyingOfficerResponseId != (int)ECertifyingOfficerResponse.No
                ? "ConfirmGcoDetails"
                : "HasGrantCertifyingOfficer",
            IsProgressReportGcoComplete = isGcoComplete,
            ProgressReportVersion = version
        };
    }
}

public class GetGrantCertifyingOfficerAddressRequest : IRequest<GetGrantCertifyingOfficerAddressResponse>
{
    private GetGrantCertifyingOfficerAddressRequest()
    {
    }

    public static readonly GetGrantCertifyingOfficerAddressRequest Request = new();
}

public class GetGrantCertifyingOfficerAddressResponse
{
    public string NameNumber { get; set; }
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string City { get; set; }
    public string County { get; set; }
    public string PostCode { get; set; }
    public string ReturnUrl { get; set; }
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
    public int ProgressReportVersion { get; set; }
    public bool IsProgressReportGcoComplete { get; set; }
}