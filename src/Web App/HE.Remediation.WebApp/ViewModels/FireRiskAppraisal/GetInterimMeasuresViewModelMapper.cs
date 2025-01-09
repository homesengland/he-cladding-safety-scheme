using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.InterimMeasures.GetInterimMeasures;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.InterimMeasures.SetInterimMeasures;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAppraisal
{
    public class GetInterimMeasuresViewModelMapper : Profile
    {
        public GetInterimMeasuresViewModelMapper()
        {
            CreateMap<GetInterimMeasuresResponse, GetInterimMeasuresViewModel>();
            CreateMap<GetInterimMeasuresViewModel, SetInterimMeasuresRequest>();
        }
    }
}
