using AutoMapper;
using HE.Remediation.Core.Data.StoredProcedureResults.FireRiskAppraisal;

namespace HE.Remediation.WebApp.ViewModels.Shared
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