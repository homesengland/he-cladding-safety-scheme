using System.Collections.Generic;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingsInsurance.GetBuildingsInsurance;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingsInsurance.SetBuildingsInsurance;

namespace HE.Remediation.Core.Data.Repositories;

public interface IBuildingsInsuranceRepository
{
    Task<GetBuildingsInsuranceResponse> GetBuildingsInsurance(Guid applicationId);
    Task<UseCase.Areas.ClosingReport.BuildingsInsurance.GetBuildingsInsurance.GetBuildingsInsuranceResponse> GetClosingReportBuildingsInsurance(Guid applicationId);

    Task<List<InsuranceProvider>> GetBuildingInsuranceProviders();
    Task<SetBuildingsInsuranceResponse> SaveBuildingInsurance(SetBuildingsInsuranceRequest request);
    Task<UseCase.Areas.ClosingReport.BuildingsInsurance.SetBuildingsInsurance.SetBuildingsInsuranceResponse> SaveClosingReportBuildingInsurance(UseCase.Areas.ClosingReport.BuildingsInsurance.SetBuildingsInsurance.SetBuildingsInsuranceRequest request);
}
