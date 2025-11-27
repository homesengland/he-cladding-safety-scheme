using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.FireRiskAssessment;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAssessment;

public class AboutThisSectionViewModelMapper : Profile
{
    public AboutThisSectionViewModelMapper()
    {
        CreateMap<GetAboutThisSectionResponse, AboutThisSectionViewModel>();
    }   
}