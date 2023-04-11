using HE.Remediation.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HE.Remediation.Core.UseCase.Areas.Administration.UserContactConsent.GetUserContactConsent;

public class GetUserContactConsentResponse
{
    public ENoYes? UserConsent { get; set; }
}
