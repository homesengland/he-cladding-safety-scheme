using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.AddInternalWallWorks;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAppraisal;

public class InternalWorksDetailViewModelMapper: Profile
{
    public InternalWorksDetailViewModelMapper()
    {
        CreateMap<GetAddInternalWallWorksResponse, InternalWorksDetailViewModel>();            
        CreateMap<InternalWorksDetailViewModel , SetAddInternalWallWorksRequest>();            
    }    
}
