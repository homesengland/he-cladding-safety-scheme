using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Data.StoredProcedureParameters.WorkPackage.GrantCertifyingOfficer;
using HE.Remediation.Core.UseCase.Areas.Location.PostCode;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting;

public class SetGrantCertifyingOfficerAddressResultHandler : IRequestHandler<SetGrantCertifyingOfficerAddressResultRequest>
{
    private readonly IProgressReportingRepository _progressReportingRepository;

    public SetGrantCertifyingOfficerAddressResultHandler(IProgressReportingRepository progressReportingRepository)
    {
        _progressReportingRepository = progressReportingRepository;
    }

    public async ValueTask<Unit> Handle(SetGrantCertifyingOfficerAddressResultRequest request, CancellationToken cancellationToken)
    {
        var parsedAddress = PostCodeUtility.ParseAddressJson(request.SelectedAddressId);
        if (parsedAddress != null)
        {
            await _progressReportingRepository.UpdateGrantCertifyingOfficerAddress(
                new UpdateGrantCertifyingOfficerAddressParameters
                {
                    NameNumber = parsedAddress.NameNumber,
                    AddressLine1 = parsedAddress.AddressLine1,
                    AddressLine2 = parsedAddress.AddressLine2,
                    City = parsedAddress.City,
                    LocalAuthority = parsedAddress.LocalAuthority,
                    County = parsedAddress.County,
                    Postcode = parsedAddress.Postcode,
                    SubBuildingName = parsedAddress.SubBuildingName,
                    BuildingName = parsedAddress.BuildingName,
                    BuildingNumber = parsedAddress.BuildingNumber,
                    Street = parsedAddress.Street,
                    Town = parsedAddress.Town,
                    AdminArea = parsedAddress.AdminArea,
                    UPRN = parsedAddress.UPRN,
                    AddressLines = parsedAddress.AddressLines,
                    XCoordinate = parsedAddress.XCoordinate,
                    YCoordinate = parsedAddress.YCoordinate,
                    Toid = parsedAddress.Toid,
                    BuildingType = parsedAddress.BuildingType
                });
        }

        return Unit.Value;
    }
}

public class SetGrantCertifyingOfficerAddressResultRequest : IRequest
{
    public string SelectedAddressId { get; set; }
}