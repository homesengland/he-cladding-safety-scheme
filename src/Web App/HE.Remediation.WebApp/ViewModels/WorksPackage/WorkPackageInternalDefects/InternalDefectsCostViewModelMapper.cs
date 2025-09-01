using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageInternalDefects.InternalDefectsCost.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageInternalDefects.InternalDefectsCost.Set;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageInternalDefects.StartInformation;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageInternalDefects;

public class InternalDefectsCostViewModelMapper : Profile
{
    public InternalDefectsCostViewModelMapper()
    {
        CreateMap<GetInternalDefectsCostResponse, InternalDefectsCostViewModel>();
        CreateMap<InternalDefectsCostViewModel, SetInternalDefectsCostRequest>();
    }
}