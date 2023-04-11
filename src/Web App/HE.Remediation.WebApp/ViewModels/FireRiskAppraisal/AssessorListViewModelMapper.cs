using AutoMapper;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.AssessorList.GetAssessorList;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAppraisal
{
    public class AssessorListViewModelMapper : Profile
    {
        public AssessorListViewModelMapper()
        {
            CreateMap<GetAssessorListResponse, AssessorListViewModel>();
            CreateMap<GetFireRiskAssessorListResult, AssessorCompanyViewModel>();
        }
    }
}