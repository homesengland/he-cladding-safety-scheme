using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Data.StoredProcedureParameters.BuildingDetails;
using HE.Remediation.Core.Data.StoredProcedureResults.BuildingDetails;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.ProvideBuildingAddress.GetBuildingAddress;

namespace HE.Remediation.Core.Data.Repositories;

public interface IBuildingDetailsRepository
{
    Task<GetBuildingAddressResponse> GetBuildingAddress(Guid applicationId);

    Task InsertBuildingAddress(BuildingDetailsAddressDetails details, Guid applicationId);

    Task UpdateBuildingAddress(BuildingDetailsAddressDetails details, Guid applicationId);

    Task<string> GetBuildingUniqueName(Guid applicationId);

    Task<BuildingDetailsKeyDatesResult> GetBuildingDetailsKeyDates(Guid applicationId);

    Task UpdateBuildingDetailsKeyDates(UpdateBuildingDetailsKeyDatesParameters parameters);

    Task<DateTime?> GetConstructionCompletionDate(Guid applicationId);

    Task UpdateConstructionCompletionDate(UpdateConstructionCompletionDateParameters parameters);

    Task<RefurbishmentCompletionDateResult> GetRefurbishmentCompletionDate(Guid applicationId);

    Task UpdateRefurbishmentCompletionDate(UpdateRefurbishmentCompletionDateParameters parameters);
}
