using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.Location;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.ProvideBuildingAddress.GetBuildingAddress;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.ProvideBuildingAddress.SetBuildingAddressManual;

public class SetBuildingAddressManualHandler: IRequestHandler<SetBuildingAddressManualRequest, Unit>
{
    private readonly IDbConnectionWrapper _dbConnectionWrapper;
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IPostCodeLookup _postCodeLookup;
        private readonly IBuildingDetailsRepository _buildingDetailsRepo;

        public SetBuildingAddressManualHandler(IDbConnectionWrapper dbConnectionWrapper, 
                                         IApplicationDataProvider applicationDataProvider,
                                         IPostCodeLookup postCodeLookup, 
                                         IBuildingDetailsRepository buildingDetailsRepo)
        {
            _dbConnectionWrapper = dbConnectionWrapper;
            _applicationDataProvider = applicationDataProvider;
            _postCodeLookup = postCodeLookup;
            _buildingDetailsRepo = buildingDetailsRepo;
        }

        public async ValueTask<Unit> Handle(SetBuildingAddressManualRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            BuildingDetailsAddressDetails details = new BuildingDetailsAddressDetails
            {
                NameNumber = request.NameNumber,
                AddressLine1 = request.AddressLine1,
                AddressLine2 = request.AddressLine2,
                City = request.City,
                County = request.County,
                Postcode = request.Postcode,
                LocalAuthority = request.LocalAuthority
            };

            var response = await _buildingDetailsRepo.GetBuildingAddress(applicationId);            
            if (response is not null)
            {                    
                await _buildingDetailsRepo.UpdateBuildingAddress(details, applicationId);
                return Unit.Value;
            }

            await _buildingDetailsRepo.InsertBuildingAddress(details, applicationId);
            
            return Unit.Value;
        }
}
