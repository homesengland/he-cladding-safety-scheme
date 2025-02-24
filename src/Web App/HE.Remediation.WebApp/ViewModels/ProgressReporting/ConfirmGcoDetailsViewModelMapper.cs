using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class ConfirmGcoDetailsViewModelMapper : Profile
{
    public ConfirmGcoDetailsViewModelMapper()
    {
        CreateMap<GetGcoDetailsResponse, ConfirmGcoDetailsViewModel>();
        CreateMap<ConfirmGcoDetailsViewModel, SetGcoDetailsRequest>();
    }
}