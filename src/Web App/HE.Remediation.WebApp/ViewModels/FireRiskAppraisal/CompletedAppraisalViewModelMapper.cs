using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.CompletedAppraisal.GetCompletedAppraisal;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.CompletedAppraisal.SetCompletedAppraisal;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAppraisal
{
    public class CompletedAppraisalViewModelMapper : Profile
    {
        public CompletedAppraisalViewModelMapper()
        {
            CreateMap<CompletedAppraisalViewModel, SetCompletedAppraisalRequest>();
            CreateMap<GetCompletedAppraisalResponse, CompletedAppraisalViewModel>();
        }
    }
}