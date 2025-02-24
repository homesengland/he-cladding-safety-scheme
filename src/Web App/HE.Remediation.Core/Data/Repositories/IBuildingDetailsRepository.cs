﻿using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.ProvideBuildingAddress.GetBuildingAddress;

namespace HE.Remediation.Core.Data.Repositories;

public interface IBuildingDetailsRepository
{
    Task<GetBuildingAddressResponse> GetBuildingAddress(Guid applicationId);

    Task InsertBuildingAddress(BuildingDetailsAddressDetails details, Guid applicationId);

    Task UpdateBuildingAddress(BuildingDetailsAddressDetails details, Guid applicationId);

    Task<string> GetBuildingUniqueName(Guid applicationId);
}
