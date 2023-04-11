using AutoMapper;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.UseCase.Areas.AlternativeFundingRoutes.PursuedSourcesFunding.GetPursuedSourcesFunding;
using HE.Remediation.Core.UseCase.Areas.AlternativeFundingRoutes.PursuedSourcesFunding.SetPursuedSourcesFunding;

namespace HE.Remediation.WebApp.ViewModels.AlternativeFundingRoutes;

public class PursuedSourcesFundingViewModelMapper : Profile
{
    public PursuedSourcesFundingViewModelMapper()
    {
        CreateMap<PursuedSourcesFundingViewModel, SetPursuedSourcesFundingRequest>()
            .ForMember(
                x => x.CompleteSection, 
                o => o.MapFrom(s => MapCompleteSection(s)));
            
        CreateMap<GetPursuedSourcesFundingResponse, PursuedSourcesFundingViewModel>();
    }

    private static bool MapCompleteSection(PursuedSourcesFundingViewModel model)
    {
        return model.SubmitAction == ESubmitAction.Continue
               && model.PursuedSourcesFunding != EPursuedSourcesFundingType.PursuingOtherRoutes;
    }
}