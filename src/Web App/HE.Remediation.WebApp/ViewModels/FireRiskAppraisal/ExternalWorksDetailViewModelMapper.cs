using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.AddExternalWallWorks;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAppraisal;

public class ExternalWorksDetailViewModelMapper: Profile
{
    public ExternalWorksDetailViewModelMapper()
    {
        CreateMap<GetAddExternalWallWorksResponse, ExternalWorksDetailViewModel>();            
        CreateMap< ExternalWorksDetailViewModel , SetAddExternalWallWorksRequest>();            
    }
}
