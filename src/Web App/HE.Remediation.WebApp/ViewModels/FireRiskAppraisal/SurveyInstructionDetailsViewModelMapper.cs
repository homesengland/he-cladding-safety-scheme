using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.SurveyInstructionDetails.GetSurveyInstructionDetails;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.SurveyInstructionDetails.SetSurveyInstructionDetails;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAppraisal
{
    public class SurveyInstructionDetailsViewModelMapper : Profile
    {
        public SurveyInstructionDetailsViewModelMapper()
        {
            CreateMap<GetSurveyInstructionDetailsResponse, SurveyInstructionDetailsViewModel>();

            CreateMap<SurveyInstructionDetailsViewModel, SetSurveyInstructionDetailsRequest>();
        }
    }
}