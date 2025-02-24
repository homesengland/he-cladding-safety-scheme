using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.BaseInformation.Get;

namespace HE.Remediation.WebApp.ViewModels.ScheduleOfWorks;

public class FundingInformationViewModelMapper : Profile
{
    public FundingInformationViewModelMapper()
    {
        CreateMap<GetBaseInformationResponse, FundingInformationViewModel>();
    }
}
