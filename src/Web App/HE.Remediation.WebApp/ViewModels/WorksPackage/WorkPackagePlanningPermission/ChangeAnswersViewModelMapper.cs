﻿using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.BaseInformation.Get;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackagePlanningPermission;

public class ChangeAnswersViewModelMapper : Profile
{
    public ChangeAnswersViewModelMapper()
    {
        CreateMap<GetBaseInformationResponse, ChangeAnswersViewModel>();
    }
}
