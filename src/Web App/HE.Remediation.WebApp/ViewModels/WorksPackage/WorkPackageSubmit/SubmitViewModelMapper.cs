using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageSubmit.Submit.Get;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageSubmit;

public class SubmitViewModelMapper : Profile
{
    public SubmitViewModelMapper()
    {
        CreateMap<GetSubmitResponse, SubmitViewModel>();
    }
}
