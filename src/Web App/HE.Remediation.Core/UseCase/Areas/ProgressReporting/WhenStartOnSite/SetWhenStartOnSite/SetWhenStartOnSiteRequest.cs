using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.WhenStartOnSite.SetWhenStartOnSite;

public class SetWhenStartOnSiteRequest : IRequest<SetWhenStartOnSiteResponse>
{
    public int? StartMonth { get; set; }

    public int? StartYear { get; set; }
}

