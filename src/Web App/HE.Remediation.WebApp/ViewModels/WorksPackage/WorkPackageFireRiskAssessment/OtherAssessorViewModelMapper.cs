using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageFireRiskAssessment;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageFireRiskAssessment;

public class OtherAssessorViewModelMapper : Profile
{
    public OtherAssessorViewModelMapper()
    {
        CreateMap<GetOtherAssessorResponse, OtherAssessorViewModel>();
        CreateMap<OtherAssessorViewModel, SetOtherAssessorRequest>();
    }
}