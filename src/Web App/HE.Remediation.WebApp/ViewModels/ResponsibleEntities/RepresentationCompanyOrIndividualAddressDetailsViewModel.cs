﻿using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities;

public class RepresentationCompanyOrIndividualAddressDetailsViewModel : AddressViewModel
{
    public EResponsibleEntityType ResponsibleEntityType { get; set; }

    public ESubmitAction SubmitAction { get; set; }
    public string ReturnUrl { get; set; }
}