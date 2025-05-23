﻿using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.ProvideBuildingAddress.GetBuildingAddress;
using System.Transactions;

namespace HE.Remediation.Core.Data.Repositories;

public class BuildingDetailsRepository : IBuildingDetailsRepository
{
    private readonly IDbConnectionWrapper _db;

    public BuildingDetailsRepository(IDbConnectionWrapper db)
    {
        _db = db;
    }

    public async Task<GetBuildingAddressResponse> GetBuildingAddress(Guid applicationId)
    {
        return await _db.QuerySingleOrDefaultAsync<GetBuildingAddressResponse>("GetBuildingAddress", new { applicationId });
    }

    public async Task InsertBuildingAddress(BuildingDetailsAddressDetails details, Guid applicationId)
    {
        var addressId = Guid.NewGuid();

        using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            await _db.ExecuteAsync("InsertBuildingAddress",
                new
                {
                    addressId,
                    details.NameNumber,
                    details.AddressLine1,
                    details.AddressLine2,
                    details.City,
                    details.County,
                    details.Postcode,
                    details.LocalAuthority,
                    details.SubBuildingName,
                    details.BuildingName,
                    details.BuildingNumber,
                    details.Street,
                    details.Town,
                    details.AdminArea,
                    details.UPRN,
                    details.AddressLines,
                    details.XCoordinate,
                    details.YCoordinate,
                    details.Toid,
                    details.BuildingType
                }
            );

            await _db.ExecuteAsync("UpdateBuildingDetailsAddressId", new { applicationId, addressId });

            scope.Complete();
        }
    }

    public async Task UpdateBuildingAddress(BuildingDetailsAddressDetails details, Guid applicationId)
    {
        await _db.ExecuteAsync("UpdateBuildingAddress",
            new
            {
                applicationId,
                details.NameNumber,
                details.AddressLine1,
                details.AddressLine2,
                details.City,
                details.County,
                details.Postcode,
                details.LocalAuthority,
                details.SubBuildingName,
                details.BuildingName,
                details.BuildingNumber,
                details.Street,
                details.Town,
                details.AdminArea,
                details.UPRN,
                details.AddressLines,
                details.XCoordinate,
                details.YCoordinate,
                details.Toid,
                details.BuildingType
            }
        );
    }

    public async Task<string> GetBuildingUniqueName(Guid applicationId)
    {
        var uniqueName = await _db.QuerySingleOrDefaultAsync<string>(nameof(GetBuildingUniqueName), new { ApplicationId = applicationId });
        return uniqueName;
    }
}
