using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.FireRiskAssessment;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAssessment;

public class OtherAssessorViewModelMapper : Profile
{
    public OtherAssessorViewModelMapper()
    {
        CreateMap<GetOtherAssessorResponse, OtherAssessorViewModel>();
        CreateMap<OtherAssessorViewModel, SetOtherAssessorRequest>();
    }
}