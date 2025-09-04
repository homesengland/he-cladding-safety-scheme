using AutoMapper;
using HE.Remediation.Core.Data.StoredProcedureResults.WorkPackage.CostsScheduling;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCladdingSystem.FireRiskAppraisalToExternalWalls.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCladdingSystem.FireRiskAppraisalToExternalWalls.Set;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageCladdingSystem;

public class FireRiskAppraisalToExternalWallsViewModelMapper : Profile
{
    public FireRiskAppraisalToExternalWallsViewModelMapper()
    {
        CreateMap<GetFireRiskAppraisalToExternalWallsResponse, FireRiskAppraisalToExternalWallsViewModel>();
        CreateMap<CostsScheduleFireRiskCladdingSystemItemResult, CladdingSystemSummaryViewModel>();
        CreateMap<FireRiskAppraisalToExternalWallsViewModel, SetFireRiskAppraisalToExternalWallsRequest>();
    }
}
