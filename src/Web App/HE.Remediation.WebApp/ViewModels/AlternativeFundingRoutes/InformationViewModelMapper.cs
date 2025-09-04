using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.AlternativeFundingRoutes;

namespace HE.Remediation.WebApp.ViewModels.AlternativeFundingRoutes;

public class InformationViewModelMapper : Profile
{
    public InformationViewModelMapper()
    {
        CreateMap<GetAlternateFundingInformationResponse, InformationViewModel>();
    }
}