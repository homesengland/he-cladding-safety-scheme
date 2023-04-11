using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.Application.ExistingApplication.GetExistingApplication;

namespace HE.Remediation.WebApp.ViewModels.Application
{
    public class ExistingApplicationViewModelMapper : Profile
    {
        public ExistingApplicationViewModelMapper()
        {
            CreateMap<GetExistingApplicationResponse, ApplicationViewModel>();
        }
    }
}
