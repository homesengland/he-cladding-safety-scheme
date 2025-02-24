namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.SetThirdPartyContributionsChanged;

public class SetThirdPartyContributionsChangedResponse
{
    public bool? ThirdPartyContributionsChanged { get; set; }

    public bool? CostsChanged { get; set; }

    public bool? UnsafeCladdingRemoved { get; set; }

    public bool EndDateSlipped { get; set; }
}
