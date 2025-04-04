﻿using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.BuildingDetails;

public class DeveloperInBusinessViewModel
{
    public string ReturnUrl { get; set; }
    public EApplicationDeveloperInBusinessType? IsOriginalDeveloperStillInBusiness { get; set; }
    public ESubmitAction SubmitAction { get; set; }
}