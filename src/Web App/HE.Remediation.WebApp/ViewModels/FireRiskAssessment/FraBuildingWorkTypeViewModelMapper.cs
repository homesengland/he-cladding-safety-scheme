using AutoMapper;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.UseCase.Areas.FireRiskAssessment;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAssessment;

public class FraBuildingWorkTypeViewModelMapper : Profile
{
    public FraBuildingWorkTypeViewModelMapper()
    {
        CreateMap<GetBuildingWorkTypeResponse, FraBuildingWorkTypeViewModel>();
        CreateMap<FraBuildingWorkTypeViewModel, SetBuildingWorkTypeRequest>();
    }   
}