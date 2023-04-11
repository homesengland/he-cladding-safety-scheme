using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.Application.Submit.GetSubmit;

namespace HE.Remediation.WebApp.ViewModels.Application
{
    public class SubmittedModelMapper :Profile
    {
        public SubmittedModelMapper()
        {
            CreateMap<GetSubmitResponse, SubmittedViewModel>();
        }
    }
}
