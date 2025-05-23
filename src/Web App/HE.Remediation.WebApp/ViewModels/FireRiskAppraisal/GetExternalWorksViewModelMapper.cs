﻿using AutoMapper;
using HE.Remediation.Core.Data.StoredProcedureResults.FireRiskAppraisal;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAppraisal;

public class GetExternalWorksViewModelMapper : Profile
{
    public GetExternalWorksViewModelMapper()
    {        
        CreateMap<GetWallWorksListResult, GetExternalWorksViewModel.ExternalWallWorks>();            
    }
}
