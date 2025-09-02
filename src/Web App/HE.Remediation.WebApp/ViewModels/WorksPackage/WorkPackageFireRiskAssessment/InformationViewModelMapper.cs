using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageFireRiskAssessment;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageFireRiskAssessment;

public class InformationViewModelMapper : Profile
{
    public InformationViewModelMapper()
    {
        CreateMap<GetInformationResponse, InformationViewModel>();
    }
}