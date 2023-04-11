using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.NameOfDevelopment.GetNameOfDevelopment;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.NameOfDevelopment.SetNameOfDevelopment;

namespace HE.Remediation.WebApp.ViewModels.BuildingDetails
{
    public class NameOfDevelopmentViewModelMapper : Profile
    {
        public NameOfDevelopmentViewModelMapper()
        {
            CreateMap<NameOfDevelopmentViewModel, SetNameOfDevelopmentRequest>();
            CreateMap<GetNameOfDevelopmentResponse, NameOfDevelopmentViewModel>();
        }
    }
}
