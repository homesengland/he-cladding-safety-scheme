using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.Location;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.ProvideBuildingAddress.GetBuildingAddress;
using HE.Remediation.Core.UseCase.Areas.Location.PostCode;
using MediatR;
using System.Transactions;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.ProvideBuildingAddress.SetBuildingAddress;

public class SetBuildingAddressHandler : IRequestHandler<SetBuildingAddressRequest, Unit>
{
    private readonly IDbConnectionWrapper _dbConnectionWrapper;
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IPostCodeLookup _postCodeLookup;
    private readonly IBuildingDetailsRepository _buildingDetailsRepo;

    public SetBuildingAddressHandler(IDbConnectionWrapper dbConnectionWrapper,
                                     IApplicationDataProvider applicationDataProvider,
                                     IPostCodeLookup postCodeLookup,
                                     IBuildingDetailsRepository buildingDetailsRepo)
    {
        _dbConnectionWrapper = dbConnectionWrapper;
        _applicationDataProvider = applicationDataProvider;
        _postCodeLookup = postCodeLookup;
        _buildingDetailsRepo = buildingDetailsRepo;
    }

    public async Task<Unit> Handle(SetBuildingAddressRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var parsedAddress = PostCodeUtility.ParseAddressJson(request.SelectedAddressId);
        if (parsedAddress != null)
        {
            BuildingDetailsAddressDetails details = new BuildingDetailsAddressDetails
            {
                NameNumber = parsedAddress.NameNumber,
                AddressLine1 = parsedAddress.AddressLine1,
                AddressLine2 = parsedAddress.AddressLine2,
                City = parsedAddress.City,
                County = parsedAddress.County,
                Postcode = parsedAddress.Postcode
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