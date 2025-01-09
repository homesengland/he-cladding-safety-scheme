using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.WorksContract.Add;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.WorksContract.Get;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.WorksContract.Set;

namespace HE.Remediation.WebApp.ViewModels.ScheduleOfWorks;

public class UploadWorksContractViewModelMapper : Profile
{
    public UploadWorksContractViewModelMapper()
    {
        CreateMap<GetWorksContractResponse, UploadWorksContractViewModel>();
        CreateMap<UploadWorksContractViewModel, AddWorksContractRequest>();
        CreateMap<UploadWorksContractViewModel, SetWorksContractRequest>();
    }
}
