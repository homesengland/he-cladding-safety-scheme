using AutoMapper;
using HE.Remediation.Core.Data.StoredProcedureResults;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAppraisal
{
    public class FireRiskAssessorCompanyViewModelMapper : Profile
    {
        public FireRiskAssessorCompanyViewModelMapper()
        {
            CreateMap<FireRiskAssessorCompanyViewModel, GetFireRiskAssessorListResult>();

            CreateMap<GetFireRiskAssessorListResult, FireRiskAssessorCompanyViewModel>();
        }
    }
}