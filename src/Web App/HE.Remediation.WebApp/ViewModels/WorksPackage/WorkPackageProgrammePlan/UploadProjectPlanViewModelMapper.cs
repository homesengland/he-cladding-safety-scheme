using AutoMapper;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageProgrammePlan;
using File = HE.Remediation.WebApp.ViewModels.Shared.File;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageProgrammePlan;

public class UploadProjectPlanViewModelMapper : Profile
{
    public UploadProjectPlanViewModelMapper()
    {
        CreateMap<FileResult, File>();
        CreateMap<GetUploadProjectPlanResponse, UploadProjectPlanViewModel>();
        CreateMap<UploadProjectPlanViewModel, SetUploadProjectPlanRequest>();
    }
}