using AutoMapper;

using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.BaseInformation.Get;
namespace HE.Remediation.WebApp.ViewModels.ScheduleOfWorks;

public class ProfileCostsStartInformationViewModelMpper : Profile
{
    public ProfileCostsStartInformationViewModelMpper()
    {
        CreateMap<GetBaseInformationResponse, ProfileCostsStartInformationViewModel>();
    }
}