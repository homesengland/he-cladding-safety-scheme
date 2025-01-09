using AutoMapper;
using HE.Remediation.Core.Data.StoredProcedureResults.FireRiskAppraisal;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.WorksToCladdingSystems.CladdingArea;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.WorksToCladdingSystems.GetCladdingSystem;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.WorksToCladdingSystems.GetWorksToCladdingSystems;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.WorksToCladdingSystems.SetCladdingSystem;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAppraisal
{
    public class CladdingSystemViewModelMapper : Profile
    {
        public CladdingSystemViewModelMapper()
        {
            CreateMap<GetCladdingSystemResponse, CladdingSystemViewModel>();
            CreateMap<GetCladdingTypeResult, CladdingTypeViewModel>();
            CreateMap<GetInsulationTypeResult, InsulationTypeViewModel>();
            CreateMap<GetCladdingManufacturerResult, CladdingManufacturerViewModel>();
            CreateMap<CladdingSystemViewModel, SetCladdingSystemRequest>();
            CreateMap<GetTotalCladdingAreaResponse, WorksToCladdingCladdingAreaViewModel>();
        }
    }
}