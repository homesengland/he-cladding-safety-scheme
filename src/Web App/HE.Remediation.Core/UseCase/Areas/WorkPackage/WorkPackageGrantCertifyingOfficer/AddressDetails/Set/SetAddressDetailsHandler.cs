using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters.WorkPackage.GrantCertifyingOfficer;
using HE.Remediation.Core.UseCase.Areas.Location.PostCode;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageGrantCertifyingOfficer.AddressDetails.Set;

public class SetAddressDetailsHandler : IRequestHandler<SetAddressDetailsRequest>
{
    private readonly IWorkPackageRepository _workPackageRepository;

    public SetAddressDetailsHandler(IWorkPackageRepository workPackageRepository)
    {
        _workPackageRepository = workPackageRepository;
    }

    public async ValueTask<Unit> Handle(SetAddressDetailsRequest request, CancellationToken cancellationToken)
    {
        var parsedAddress = PostCodeUtility.ParseAddressJson(request.SelectedAddressId);
        if (parsedAddress != null)
        {
            await _workPackageRepository.UpdateWorkPackageGrantCertifyingOfficerAddress(
                new UpdateWorkPackageGrantCertifyingOfficerAddressParameters
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
