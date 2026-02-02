using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageDutyOfCareDeed.Progress.Get;

public class GetProgressRequest : IRequest<GetProgressResponse>
{
	private GetProgressRequest()
	{
	}

	public static readonly GetProgressRequest Request = new();
}
