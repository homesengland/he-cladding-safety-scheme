using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageFireRiskAssessment;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageFireRiskAssessment;

public class IdentifiedDefectsViewModelMapper : Profile
{
    public IdentifiedDefectsViewModelMapper()
    {
        CreateMap<GetIdentifiedDefectsResponse, IdentifiedDefectsViewModel>();
        CreateMap<IdentifiedDefectsViewModel, SetIdentifiedDefectsRequest>();
    }
}