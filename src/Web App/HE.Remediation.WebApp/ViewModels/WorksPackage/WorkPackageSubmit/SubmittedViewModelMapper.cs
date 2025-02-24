using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageSubmit.Submitted.Get;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageSubmit;

public class SubmittedViewModelMapper : Profile
{
    public SubmittedViewModelMapper()
    {
        CreateMap<GetSubmittedResponse, SubmittedViewModel>();
    }
}
