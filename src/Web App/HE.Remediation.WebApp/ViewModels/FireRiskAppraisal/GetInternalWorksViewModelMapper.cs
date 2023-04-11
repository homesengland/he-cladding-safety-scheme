using AutoMapper;
using HE.Remediation.Core.Data.StoredProcedureResults.FireRiskAppraisal;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAppraisal;

public class GetInternalWorksViewModelMapper: Profile
{
    public GetInternalWorksViewModelMapper()
    {        
        CreateMap<GetWallWorksListResult, GetInternalWorksViewModel.InternalWallWorks>();            
    }
}
