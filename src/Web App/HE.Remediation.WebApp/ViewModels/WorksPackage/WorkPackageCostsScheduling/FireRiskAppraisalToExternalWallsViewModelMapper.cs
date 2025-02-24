using AutoMapper;
using HE.Remediation.Core.Data.StoredProcedureResults.WorkPackage.CostsScheduling;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.FireRiskAppraisalToExternalWalls.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.FireRiskAppraisalToExternalWalls.Set;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageCostsScheduling;

public class FireRiskAppraisalToExternalWallsViewModelMapper : Profile
{
    public FireRiskAppraisalToExternalWallsViewModelMapper()
    {
        CreateMap<GetFireRiskAppraisalToExternalWallsResponse, FireRiskAppraisalToExternalWallsViewModel>();
        CreateMap<CostsScheduleFireRiskCladdingSystemItemResult, CladdingSystemSummaryViewModel>();
        CreateMap<FireRiskAppraisalToExternalWallsViewModel, SetFireRiskAppraisalToExternalWallsRequest>();
    }
}
