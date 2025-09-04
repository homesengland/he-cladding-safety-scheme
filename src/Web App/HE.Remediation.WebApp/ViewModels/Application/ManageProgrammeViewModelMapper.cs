using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.Application.ManageProgramme;

namespace HE.Remediation.WebApp.ViewModels.Application
{
    public class ManageProgrammeViewModelMapper : Profile
    {
        public ManageProgrammeViewModelMapper()
        {
            CreateMap<GetManageProgrammeResponse, ManageProgrammeViewModel.ApplicationViewModel>();
        }
    }
}
