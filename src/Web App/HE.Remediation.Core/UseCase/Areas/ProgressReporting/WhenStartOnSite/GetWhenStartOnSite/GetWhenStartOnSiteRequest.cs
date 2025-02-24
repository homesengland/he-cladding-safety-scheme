using MediatR;
namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.WhenStartOnSite.GetWhenStartOnSite;

    public class GetWhenStartOnSiteRequest : IRequest<GetWhenStartOnSiteResponse>
    {
        private GetWhenStartOnSiteRequest()
        {
        }

        public static readonly GetWhenStartOnSiteRequest Request = new();
    }


