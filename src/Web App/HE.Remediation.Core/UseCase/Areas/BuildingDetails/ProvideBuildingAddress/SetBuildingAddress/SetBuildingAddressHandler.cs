using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.ProvideBuildingAddress.GetBuildingAddress;
using HE.Remediation.Core.UseCase.Areas.Location.PostCode;
using MediatR;
using System.Transactions;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.ProvideBuildingAddress.SetBuildingAddress;

public class SetBuildingAddressHandler : IRequestHandler<SetBuildingAddressRequest, Unit>
{
    private readonly IDbConnectionWrapper _dbConnectionWrapper;
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IBuildingDetailsRepository _buildingDetailsRepo;

    public SetBuildingAddressHandler(IDbConnectionWrapper dbConnectionWrapper,
                                     IApplicationDataProvider applicationDataProvider,
                                     IBuildingDetailsRepository buildingDetailsRepo)
    {
        _dbConnectionWrapper = dbConnectionWrapper;
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepo = buildingDetailsRepo;
    }

    public async Task<Unit> Handle(SetBuildingAddressRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var parsedAddress = PostCodeUtility.ParseBuildingLookupJson(request.SelectedAddressId);
        if (parsedAddress != null)
        {
            var details = new BuildingDetailsAddressDetails
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
            };

            // push to repo
            var response = await _dbConnectionWrapper.QuerySingleOrDefaultAsync<GetBuildingAddressResponse>("GetBuildingAddress", new { applicationId });
            if (response is not null)
            {
                await _buildingDetailsRepo.UpdateBuildingAddress(details, applicationId);
                return Unit.Value;
            }

            await _buildingDetailsRepo.InsertBuildingAddress(details, applicationId);
        }

        return Unit.Value;
    }    
}