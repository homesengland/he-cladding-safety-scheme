using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.InformedLeaseholder.GetInformedLeaseholder;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.InformedLeaseholder.SetInformedLeaseholder;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class InformedLeaseholderViewModelMapper : Profile
{
    public InformedLeaseholderViewModelMapper()
    {
        CreateMap<GetInformedLeaseholderResponse, InformedLeaseholderViewModel>();
        CreateMap<InformedLeaseholderViewModel, SetInformedLeaseholderRequest>();
    }       
}
