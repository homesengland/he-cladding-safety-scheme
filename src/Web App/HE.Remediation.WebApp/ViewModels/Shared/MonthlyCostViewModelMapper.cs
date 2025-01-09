using AutoMapper;
using HE.Remediation.Core.Data.StoredProcedureResults.Costs;

namespace HE.Remediation.WebApp.ViewModels.Shared;

public class MonthlyCostViewModelMapper : Profile
{
    public MonthlyCostViewModelMapper()
    {
        CreateMap<MonthlyCostResult, MonthlyCostViewModel>()
            .ForMember(d => d.AmountText, 
                       o => o.MapFrom(s => s.Amount.HasValue 
                                             ? s.Amount.Value.ToString("N0") 
                                             : null));

        CreateMap<MonthlyCostViewModel, MonthlyCostResult>()
           .ForMember(d => d.MonthDate,
                      o => o.MapFrom(s => s.MonthDate.HasValue
                                            ? new DateTime(s.MonthDate.Value.Year, s.MonthDate.Value.Month, s.MonthDate.Value.Day)
                                            : (DateTime?)null));
    }
}
