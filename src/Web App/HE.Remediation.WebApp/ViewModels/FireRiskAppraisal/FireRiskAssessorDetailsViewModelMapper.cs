using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.AssessorDetails.GetAssessorDetails;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.AssessorDetails.SetAssessorDetails;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAppraisal
{
    public class FireRiskAssessorDetailsViewModelMapper: Profile
    {
        public FireRiskAssessorDetailsViewModelMapper()
        {
            CreateMap<FireRiskAssessorDetailsViewModel, SetAssessorDetailsRequest>();
            CreateMap<GetAssessorDetailsResponse, FireRiskAssessorDetailsViewModel>();
        }
    }
}
