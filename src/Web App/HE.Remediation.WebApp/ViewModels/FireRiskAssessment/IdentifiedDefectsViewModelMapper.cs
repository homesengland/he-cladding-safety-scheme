using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.FireRiskAssessment;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAssessment;

public class IdentifiedDefectsViewModelMapper : Profile
{
    public IdentifiedDefectsViewModelMapper()
    {
        CreateMap<GetIdentifiedDefectsResponse, IdentifiedDefectsViewModel>();
        CreateMap<IdentifiedDefectsViewModel, SetIdentifiedDefectsRequest>();
    }
}