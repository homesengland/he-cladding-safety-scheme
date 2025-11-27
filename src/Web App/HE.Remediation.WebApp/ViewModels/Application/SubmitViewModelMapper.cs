using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.Application.Submit.GetSubmit;

namespace HE.Remediation.WebApp.ViewModels.Application
{
    public class SubmitViewModelMapper :Profile
    {
        public SubmitViewModelMapper()
        {
            CreateMap<GetSubmitResponse, SubmitViewModel>();
        }
    }
}
