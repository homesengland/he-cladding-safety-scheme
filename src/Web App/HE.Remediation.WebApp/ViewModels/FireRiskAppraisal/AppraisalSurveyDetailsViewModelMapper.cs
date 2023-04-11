using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.AppraisalSurveyDetails.GetAppraisalSurveyDetails;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.AppraisalSurveyDetails.SetAppraisalSurveyDetails;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAppraisal
{
    public class AppraisalSurveyDetailsViewModelMapper : Profile
    {
        public AppraisalSurveyDetailsViewModelMapper()
        {
            CreateMap<GetAppraisalSurveyDetailsResponse, AppraisalSurveyDetailsViewModel>();

            CreateMap<AppraisalSurveyDetailsViewModel, SetAppraisalSurveyDetailsRequest>();
        }
    }
}