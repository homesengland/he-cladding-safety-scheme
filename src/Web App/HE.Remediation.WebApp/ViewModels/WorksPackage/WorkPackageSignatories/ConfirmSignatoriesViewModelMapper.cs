using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageSignatories.ConfirmSignatories.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageSignatories.ConfirmSignatories.Set;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageSignatories;

public class ConfirmSignatoriesViewModelMapper : Profile
{
    public ConfirmSignatoriesViewModelMapper()
    {
        CreateMap<GetConfirmSignatoriesResponse, ConfirmSignatoriesViewModel>();
        CreateMap<ConfirmSignatoriesViewModel, SetConfirmSignatoriesRequest>();
    }
}
