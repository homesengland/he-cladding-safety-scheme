using AutoMapper;
using FileSignatures.Formats;
using FluentValidation;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.LeaseholderInformedLast;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting
{
    public class LeaseholdersInformedLastViewModelMapper : Profile
    {
        public LeaseholdersInformedLastViewModelMapper()
        {
            CreateMap<GetLeaseholdersInformedLastResponse, LeaseholdersInformedLastViewModel>();
            CreateMap<LeaseholdersInformedLastViewModel, SetLeaseholdersInformedLastRequest>();
        }
    }
}
